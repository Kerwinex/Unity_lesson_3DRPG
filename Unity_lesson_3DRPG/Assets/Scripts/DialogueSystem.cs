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

        public void Dialogue(Datadialogue data)
        {
            StartCoroutine(SwitchDialogueGroup());
            StartCoroutine(ShowDialogueContent(data));
        }

        private IEnumerator SwitchDialogueGroup()
        {
            for (int i = 0; i < 10; i++) {
                groupDialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.03f);
            }
        }

        private IEnumerator ShowDialogueContent(Datadialogue data)
        {
            textContext.text = "";
            textname.text = data.NPCname[0];

            for (int i = 0; i < data.beforemission[0].Length; i++) {
                textContext.text += data.beforemission[0][i];
                yield return new WaitForSeconds(dialogueInterval);
            }
        }
    }
}

