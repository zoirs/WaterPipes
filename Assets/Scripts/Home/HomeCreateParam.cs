    using UnityEngine;

    public class HomeCreateParam {
        private GameObject prefab;
        
        public HomeCreateParam(GameObject prefab) {
            this.prefab = prefab;
        }

        public GameObject Prefab => prefab;       
}