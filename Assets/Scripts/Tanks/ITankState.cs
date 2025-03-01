namespace Game.Tanks {
	public interface ITankState {
		void Initialize(Tank tank);
		
		void OnUpdate();
		void OnFixedUpdate();
		void OnEnter();
		void OnExit();
	}
}