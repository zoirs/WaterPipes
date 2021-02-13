using UnityEngine;

namespace DefaultNamespace.Tube {
    public class TubeCreateParam {
        private GameObject prefab;
        
        public TubeCreateParam(GameObject prefab) {
            this.prefab = prefab;
        }

        public GameObject Prefab => prefab;
    }
}