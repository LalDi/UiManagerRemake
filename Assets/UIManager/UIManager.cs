using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using BlitzyUI.UIExample;

namespace BlitzyUI
{
	public class UIManager
	{
		private abstract class QueuedScreen
		{
			public Screen.Id id;
		}


		private class QueuedScreenPush : QueuedScreen
		{
			public Screen.Data data;
			public EScreenKey key;
			public PushedDelegate callback;

			public override string ToString()
			{
				return string.Format("[Push] {0}", id);
			}
		}


		private class QueuedScreenPop : QueuedScreen
		{
			public PoppedDelegate callback;

			public override string ToString()
			{
				return string.Format("[Pop] {0}", id);
			}
		}

		public delegate void PushedDelegate(Screen screen);
		public delegate void PoppedDelegate(Screen.Id id);

		private Dictionary<EScreenKey, Screen> _dicScreenPrefab;

		/// <summary>
		/// [Ryan] A fix for input order not obeying the render order of the screens.
		/// This is a bug as of Unity 2019.1.9f1
		/// </summary>
		public bool inputOrderFixEnabled = true;

		private Canvas _rootCanvas;
		private CanvasScaler _rootCanvasScalar;
		private Dictionary<EScreenKey, Screen> _cache;
		private Queue<QueuedScreen> _queue;
		private Stack<Screen> _stack;
		private HashSet<Screen.Id> _stackIdSet;
		private State _state;

		private PushedDelegate _activePushCallback;
		private PoppedDelegate _activePopCallback;

		public Vector2 ReferenceResolution { get { return _rootCanvasScalar.referenceResolution; } }

		private enum State
		{
			Ready,
			Push,
			Pop
		}

		public void Init(Canvas rootCanvas, List<ScreenKeyData> listScreenKey)
		{
			if (rootCanvas == null) throw new System.Exception("[UIManager] rootCanvas is null");
			if (listScreenKey == null || listScreenKey.Count == 0) throw new System.Exception("[UIManager] ScreenKey's List is Null or Empty");
			_rootCanvas = rootCanvas;
			_rootCanvasScalar = _rootCanvas.GetComponent<CanvasScaler>();
			if (_rootCanvasScalar == null)
			{
				throw new System.Exception(string.Format("{0} must have a CanvasScalar component attached to it for UIManager.", _rootCanvas.name));
			}

			_dicScreenPrefab = new Dictionary<EScreenKey, Screen>();
			_cache = new Dictionary<EScreenKey, Screen>();
			_queue = new Queue<QueuedScreen>();
			_stack = new Stack<Screen>();
			_state = State.Ready;

			foreach (var data in listScreenKey)
			{
				foreach (var item in data.screenList)
				{
					if (_dicScreenPrefab.ContainsKey(item.key) == false)
						_dicScreenPrefab.Add(item.key, item.prefab);
				}
			}

			// Remove any objects that may be lingering underneath the root.
			foreach (Transform child in _rootCanvas.transform)
			{
				Object.Destroy(child.gameObject);
			}
		}

		/// <summary>
		/// Queue the screen to be pushed onto the screen stack. 
		/// Callback will be invoked when the screen is pushed to the stack.
		/// </summary>
		public void QueuePush(Screen.Id id, Screen.Data data, EScreenKey key, PushedDelegate callback = null)
		{
			if (GetScreen(id) != null)
			{
				Debug.LogWarning(string.Format("Screen {0} already exists in the stack. Ignoring push request.", id));
				return;
			}

			//if (ScreenWillExist(id))
			//{
			//    Debug.LogWarning(string.Format("Screen {0} will exist in the stack after the queue is fully executed. Ignoring push request.", id));
			//    return;
			//}

			QueuedScreenPush push = new QueuedScreenPush();
			push.id = id;
			push.data = data;
			push.key = key;
			push.callback = callback;

			_queue.Enqueue(push);

			OnUpdate();
		}

		/// <summary>
		/// Queue the screen to be popped from the screen stack. This will pop all screens on top of it as well.
		/// Callback will be invoked when the screen is reached, or popped if 'include' is true.
		/// </summary>
		public void QueuePopTo(Screen.Id id, bool include, PoppedDelegate callback = null)
		{
			bool found = false;

			//for (int i = 0; i < _stack.Count; i++)
			foreach (var screen in _stack)
			{
				//var screen = _stack[i];

				if (screen.id != id)
				{
					var queuedPop = new QueuedScreenPop();
					queuedPop.id = screen.id;

					_queue.Enqueue(queuedPop);
				}
				else
				{
					if (include)
					{
						var queuedPop = new QueuedScreenPop();
						queuedPop.id = screen.id;
						queuedPop.callback = callback;

						_queue.Enqueue(queuedPop);
					}

					if (callback != null)
						callback(screen.id);

					found = true;
					break;
				}
			}

			if (!found)
				Debug.LogWarning(string.Format("[UIManager] {0} was not in the stack. All screens have been popped.", id));

			OnUpdate();
		}

		/// <summary>
		/// Queue the top-most screen to be popped from the screen stack.
		/// Callback will be invoked when the screen is popped from the stack.
		/// </summary>
		public void QueuePop(PoppedDelegate callback = null)
		{
			Screen topScreen = GetTopScreen();
			if (topScreen == null)
				return;

			QueuedScreenPop pop = new QueuedScreenPop();
			pop.id = topScreen.id;
			pop.callback = callback;

			_queue.Enqueue(pop);
			OnUpdate();
		}

		public void OnUpdate()
		{
			if (CanExecuteNextQueueItem())
				ExecuteNextQueueItem();
		}

		public Screen GetTopScreen()
		{
			if (_stack.Count > 0)
				return _stack.Peek();

			return null;
		}

		public Screen GetScreen(Screen.Id id)
		{
			//int count = _stack.Count;
			//for (int i = 0; i < count; i++)
			//{
			//    if (_stack[i].id == id)
			//        return _stack[i];
			//}

			foreach (var item in _stack)
			{
				if (item.id == id)
					return item;
			}


			return null;
		}

		public T GetScreen<T>(Screen.Id id) where T : Screen
		{
			Screen screen = GetScreen(id);
			return (T)screen;
		}

		private bool CanExecuteNextQueueItem()
		{
			if (_state == State.Ready)
			{
				if (_queue.Count > 0)
				{
					return true;
				}
			}

			return false;
		}

		private void ExecuteNextQueueItem()
		{
			// Get next queued item.
			QueuedScreen queued = _queue.Dequeue();

			if (queued is QueuedScreenPush)
			{
				// Push screen.
				QueuedScreenPush queuedPush = (QueuedScreenPush)queued;
				Screen screenInstance;

				if (_cache.TryGetValue(queuedPush.key, out screenInstance))
				{
					// Use cached instance of screen.
					_cache.Remove(queuedPush.key);

					// Move cached to the front of the transfrom heirarchy so that it is sorted properly.
					screenInstance.transform.SetAsLastSibling();

					screenInstance.gameObject.SetActive(true);
				}
				else
				{
					// Instantiate new instance of screen.
					//string path = System.IO.Path.Combine(resourcePrefabDirectory, queuedPush.prefabName);
					//Screen prefab = Resources.Load<Screen>(path);
					Screen prefab;// = _dicScreenPrefab[queuedPush.key];
					if (_dicScreenPrefab.TryGetValue(queuedPush.key, out prefab))
					{
						screenInstance = Object.Instantiate(prefab, _rootCanvas.transform);
						screenInstance.Setup(queuedPush.id, queuedPush.key);
						screenInstance.OnHierFixed();
					}
					else
					{
						throw new System.Exception("[UIManager] UIManager??? ???????????? ?????? Screen??? key??? Push???????????????.\n" +
							"key : " + queuedPush.key.ToString());
					}
				}

				// Tell previous top screen that it is losing focus.
				var topScreen = GetTopScreen();
				if (topScreen != null)
				{
					if (topScreen is Window)
					{
						var topWindow = topScreen as Window;
						topWindow.OnFocusLost();
					}
				}

				// Insert new screen at the top of the stack.
				_state = State.Push;
				_stack.Push(screenInstance);
				//_stack.Insert(0, screenInstance);

				if (this.inputOrderFixEnabled)
				{
					this.UpdateSortOrderOverrides();
				}

				_activePushCallback = queuedPush.callback;

				screenInstance.onPushFinished += HandlePushFinished;
				screenInstance.OnShowing(queuedPush.data);

				//screenInstance.EndInAnim();

				//screenInstance.onPushFinished += HandlePushFinished;
				//screenInstance.PushFinished();

				//if (_queue.Count == 0)
				//{
				//    #if PRINT_FOCUS
				//    Debug.Log(string.Format("[UIManager] Gained Focus: {0}", screenInstance.id));
				//    #endif
				//
				//    // Screen gains focus when it is on top of the screen stack and no other items in the queue.
				//    screenInstance.OnFocus();
				//}
			}
			else
			{
				// Pop screen.
				QueuedScreenPop queuedPop = (QueuedScreenPop)queued;

				if (_stack.Peek().id != queued.id)
				{
					throw new System.Exception(string.Format("The top screen does not match the queued pop. " +
															 "TopScreen: {0}, QueuedPop: {1}", _stack.Peek().id, queued.id));
				}

				Screen screenToPop = _stack.Pop(); //GetTopScreen();

				//screenToPop.OnFocusLost();
				//if (screenToPop is Window)
				//{
				//    var topPopup = (Window)screenToPop;
				//    topPopup.OnFocusLost();
				//}

				_state = State.Pop;
				//_stack.RemoveAt(0);

				// Tell new top screen that it is gaining focus.
				var newTopScreen = GetTopScreen();
				if (newTopScreen != null)
				{
					if (_queue.Count == 0)
					{
						// Screen gains focus when it is on top of the screen stack and no other items in the queue.
						if (newTopScreen is Window)
						{
							var topWindow = newTopScreen as Window;
							topWindow.OnReFocus();
						}
					}
				}

				_activePopCallback = queuedPop.callback;

				//screenToPop.OnPop();
				screenToPop.onPopFinished += HandlePopFinished;
				screenToPop.OnHiding();
				//screenToPop.EndOutAnim();

				//screenToPop.onPopFinished += HandlePopFinished;
				//screenToPop.PopFinished();
			}
		}

		private void UpdateSortOrderOverrides()
		{
			int managedOrder = _stack.Count;
			foreach (var screen in _stack)
			{
				screen.UpdateSortOrderOverrides(managedOrder--);
			}
		}

		private void HandlePushFinished(Screen screen)
		{
			screen.onPushFinished -= HandlePushFinished;

			_state = State.Ready;

			if (_activePushCallback != null)
			{
				_activePushCallback(screen);
				_activePushCallback = null;
			}

			OnUpdate();
		}

		private void HandlePopFinished(Screen screen)
		{
			screen.onPopFinished -= HandlePopFinished;

			if (screen.keepCached)
			{
				// Store in the cache for later use.
				screen.gameObject.SetActive(false);

				// TODO: Need to have a better cache storage mechanism that supports multiple screens of the same prefab?
				if (!_cache.ContainsKey(screen.key))
				{
					_cache.Add(screen.key, screen);
				}
			}
			else
			{
				// Destroy screen.
				Object.Destroy(screen.gameObject);
			}

			_state = State.Ready;

			if (_activePopCallback != null)
			{
				_activePopCallback(screen.id);
				_activePopCallback = null;
			}

			OnUpdate();
		}
	}
}