using System.Collections;
using UnityEngine;

namespace LastOneOut
{
    public class BoardItemAnimator : MonoBehaviour
    {
        [SerializeField] private Animator anim = null;
        [SerializeField] private Collider itemCollider = null;
        [SerializeField] private Color disabledColor = Color.white;
        [SerializeField] private SkinnedMeshRenderer[] renderers = null;
        [HideInInspector] public System.Action onAnimationDisappear = null;
        [HideInInspector] public System.Action onAnimationExit = null;

        public void AnimationDisappear()
        {
            if (onAnimationDisappear != null)
                onAnimationDisappear();
        }

        public void AnimationExit()
        {
            if (onAnimationExit != null)
                onAnimationExit();
        }

        public void SetInitState()
        {
            anim.SetBool("Init", true);
        }

        public void SetVictoryState()
        {
            DisableCollider();
            ChangeRenderers();
            anim.SetTrigger("Victory");
        }

        public void SetDefeateState()
        {
            DisableCollider();
            ChangeRenderers();
            anim.SetTrigger("Defeat");
        }

        private void DisableCollider()
        {
            itemCollider.enabled = false;
        }

        private void ChangeRenderers()
        {
            if (renderers == null)
                return;

            StopCoroutine("DoChangeRenderers");
            StartCoroutine("DoChangeRenderers");
        }

        private IEnumerator DoChangeRenderers()
        {
            float step = 0.0f;
            SkinnedMeshRenderer reference = renderers[0];
            Color currentColor = reference.material.GetColor("_Color");
            while (currentColor != disabledColor)
            {
                currentColor = Color.Lerp(currentColor, disabledColor, step);

                foreach (SkinnedMeshRenderer renderer in renderers)
                {
                    renderer.material.SetColor("_Color", currentColor);
                }

                yield return new WaitForEndOfFrame();

                step += 0.001f;
            }

            yield return null;
        }
    }
}
