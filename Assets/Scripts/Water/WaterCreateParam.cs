    using UnityEngine;

    public class WaterCreateParam {
        private GameObject prefab;
        
        public WaterCreateParam(GameObject prefab) {
            this.prefab = prefab;
        }

        public GameObject Prefab => prefab;       
}