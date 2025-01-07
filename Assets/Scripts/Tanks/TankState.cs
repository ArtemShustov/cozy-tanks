using UnityEngine;

namespace Game.Tanks {
	public abstract class TankState: MonoBehaviour, ITankState {
		protected TankStateMachine Tank { get; private set; }

		public void Initialize(TankStateMachine tank) {
			Tank = tank;
		}
		
		public virtual void OnUpdate() {}
		public virtual void OnFixedUpdate() {}
		public virtual void OnEnter() {}
		public virtual void OnExit() {}
	}
}