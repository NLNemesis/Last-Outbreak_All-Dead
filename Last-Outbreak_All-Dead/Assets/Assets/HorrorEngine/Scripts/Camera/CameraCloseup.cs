using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_6000_0_OR_NEWER
using Unity.Cinemachine;
#else
using Cinemachine;
#endif

namespace HorrorEngine
{
    public abstract class Closeup : MonoBehaviour
    {
        [SerializeField] protected bool m_PauseGame = true;
        [SerializeField] float m_FadeOutDuration = 0.5f;
        [SerializeField] float m_FadeInDuration = 0.5f;
        
        public abstract IEnumerator ActivationRoutine();
        public abstract IEnumerator DeactivationRoutine();

        private UIFade m_UIFade;
        private IUIInput m_UIInput;
        
        private static Coroutine m_Coroutine;
        private static Queue<CloseupRoutineEntry> m_Routines = new Queue<CloseupRoutineEntry>();

        private struct CloseupRoutineEntry
        {
            public Func<IEnumerator> Routine;
            public bool Pause;
            public bool Unpause;
        }

        // --------------------------------------------------------------------

        protected virtual void Start()
        {
            m_UIFade = UIManager.Get<UIFade>();
            m_UIInput = UIManager.Instance.GetComponent<IUIInput>();
            enabled = false;
        }

        // --------------------------------------------------------------------

        public void Activate()
        {
            m_Routines.Enqueue(new CloseupRoutineEntry()
            {
                Routine = ActivationRoutine,
                Pause = m_PauseGame,
                Unpause = false
            });

            if (m_Coroutine == null)
            {
                m_Coroutine = StartCoroutine(CloseupTranistionRoutine(0));
            }
        }

        // --------------------------------------------------------------------

        public void Deactivate()
        {
            Deactivate(0);
        }

        // --------------------------------------------------------------------

        public void Deactivate(float delay)
        {
            m_Routines.Enqueue(new CloseupRoutineEntry()
            {
                Routine = DeactivationRoutine, 
                Pause = false,
                Unpause = m_PauseGame
            });

            if (m_Coroutine == null)
            {
                m_Coroutine = StartCoroutine(CloseupTranistionRoutine(delay));
            }
        }

        // --------------------------------------------------------------------

        private IEnumerator CloseupTranistionRoutine(float delay)
        {
            if (delay > 0)
                yield return Yielders.UnscaledTime(delay);

            if (m_Routines.Peek().Pause)
                PauseController.Instance.Pause(CameraSystem.Instance);

            // Fade Out
            yield return m_UIFade.Fade(0f, 1f, m_FadeOutDuration);

            bool unpause = false;
            while (m_Routines.Count > 0)
            {
                var routineEntry = m_Routines.Dequeue();
                unpause = routineEntry.Unpause;
                yield return StartCoroutine(routineEntry.Routine?.Invoke());
            }

            if (unpause)
                PauseController.Instance.Resume(CameraSystem.Instance);

            // Fade In
            yield return m_UIFade.Fade(1f, 0f, m_FadeInDuration);

            m_Coroutine = null;

            // Check for any routines added during fadeout
            if (m_Routines.Count > 0)
            {
                m_Coroutine = StartCoroutine(CloseupTranistionRoutine(delay));
            }
        }
    }

    public class CameraCloseup : Closeup
    {
        [SerializeField] bool m_HidePlayer = false;
#if UNITY_6000_0_OR_NEWER
        [SerializeField] CinemachineCamera m_Camera;
#else
        [SerializeField] CinemachineVirtualCamera m_Camera;
#endif

        // --------------------------------------------------------------------

        protected override void Start()
        {
            base.Start();
            m_Camera.gameObject.SetActive(false);
        }

        // --------------------------------------------------------------------

        public override IEnumerator ActivationRoutine()
        {
            if (m_HidePlayer)
            {
                GameManager.Instance.Player.SetVisible(false);
            }
            m_Camera.gameObject.SetActive(true);
            yield return null;
        }

        // --------------------------------------------------------------------

        public override IEnumerator DeactivationRoutine()
        {
            if (m_HidePlayer)
            {
                GameManager.Instance.Player.SetVisible(true);
            }

            m_Camera.gameObject.SetActive(false);

            yield return null;
        }
    }
}