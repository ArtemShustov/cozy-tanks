namespace Game.Events {
	public static class EventBus<T> where T: IGameEvent {
		private static event GameEventHandler<T> _event;
		
		public static event GameEventHandler<T> Event {
			add => _event += value;
			remove => _event -= value;
		}

		public static void Raise(T gameEvent) {
			_event?.Invoke(gameEvent);
		}
		public static void AddListener(GameEventHandler<T> handler) {
			_event += handler;
		}
		public static void RemoveListener(GameEventHandler<T> handler) {
			_event -= handler;
		}
	}
}