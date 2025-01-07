namespace Game.Tanks {
	public interface ITankState {
		void Initialize(TankStateMachine tank);
		
		void OnUpdate();
		void OnFixedUpdate();
		void OnEnter();
		void OnExit();
	}
}