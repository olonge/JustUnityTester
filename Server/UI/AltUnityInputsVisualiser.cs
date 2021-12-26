using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace JustUnityTester.Server.UI {
    public class AltUnityInputsVisualiser : MonoBehaviour {
        public float VisibleTime = 1;
        [Space]
        [SerializeField] private AltUnityInputMark Template = null;

        private readonly List<AltUnityInputMark> _pool = new List<AltUnityInputMark>();
        private readonly Dictionary<int, AltUnityInputMark> _continuously = new Dictionary<int, AltUnityInputMark>();
        private Transform _transform;
        private float currentRatio;
        private float initialRatio = 1;
        public float growthBound = 2f;
        public float approachSpeed = 0.02f;

        private void Awake() {
            _transform = GetComponent<Transform>();
        }

        private IEnumerator VisualizerPulse(AltUnityInputMark mark) {
            currentRatio = initialRatio;
            while (currentRatio != growthBound) {
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);
                mark.transform.localScale = Vector2.one * currentRatio;
                yield return new WaitForEndOfFrame();
            }
            currentRatio = initialRatio;
            mark.transform.localScale = Vector2.one * currentRatio;
        }

        public void ShowClick(Vector2 pos) {
            AltUnityInputMark mark = GetMark();
            StartCoroutine(VisualizerPulse(mark));
            mark.Show(pos);
        }

        public int ShowContinuousInput(Vector2 pos, int id) {
            var currentId = id;

            AltUnityInputMark mark;
            if (_continuously.ContainsKey(currentId))
                mark = _continuously[currentId];
            else {
                mark = GetMark();
                currentId = mark.Id;
                _continuously[currentId] = mark;
            }

            mark.Show(pos);

            return currentId;
        }

        private AltUnityInputMark GetMark() {
            AltUnityInputMark inputMark;

            if (_pool.Count > 0) {
                inputMark = _pool[0];
                inputMark.gameObject.SetActive(true);
                _pool.Remove(inputMark);
            } else {
                inputMark = Instantiate(Template, _transform);
                inputMark.Init(VisibleTime, PutMark);
            }

            return inputMark;
        }

        private void PutMark(AltUnityInputMark mark) {
            if (_continuously.ContainsKey(mark.Id))
                _continuously.Remove(mark.Id);

            mark.gameObject.SetActive(false);
            _pool.Add(mark);
        }
    }
}