using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ker.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        [Header("對話系統介面物件")]
        public CanvasGroup groupDialogue;
        public Text textname;
        public Text textContext;
        public GameObject goTriangle;
        [Header("對話間隔"), Range(0, 10)]
        public float dialogueInterval = 0.3f;
        [Header("對話按鍵")]
        public KeyCode dialogueKey = KeyCode.Z;

        public void Dialogue(Datadialogue data)
        {
            StopAllCoroutines();
            StartCoroutine(SwitchDialogueGroup());
            StartCoroutine(ShowDialogueContent(data));
        }

        public void StopDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(SwitchDialogueGroup(false));
        }

        private IEnumerator SwitchDialogueGroup(bool fadeIn = true)
        {
            float increase = fadeIn ? 0.1f : -0.1f;
            
            for (int i = 0; i < 10; i++) {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(0.03f);
            }
        }

        private IEnumerator ShowDialogueContent(Datadialogue data)
        {
            textContext.text = "";            
            textname.text = "";
            textname.text = data.NPCname;
            goTriangle.SetActive(false);

            for (int j = 0; j < data.beforemission.Length; j++) {
                textContext.text = "";

                for (int i = 0; i < data.beforemission[j].Length; i++) {
                    textContext.text += data.beforemission[j][i];
                    yield return new WaitForSeconds(dialogueInterval);
                }
                goTriangle.SetActive(true);
                while (!Input.GetKeyDown(dialogueKey)) yield return null;
            }
            StartCoroutine(SwitchDialogueGroup(false));
        }
    }
}

