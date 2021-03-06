using System.Collections;
using UnityEngine;

namespace Ker.pratice
{
    /// <summary>
    /// 認識協同程序 Coroutine
    /// </summary>

    public class LearnCoroutine : MonoBehaviour
    {
        //定義協同程序方法
        //IEnumerator 為協同程序回傳值，可回傳時間
        //yield 讓步
        //new WaitForSeconds(浮點數)-等待時間
        private IEnumerator TestCoroutine()
        {
            print("協同程序開始");
            yield return new WaitForSeconds(2);
            print("協同程序等待2秒後執行");
        }

        public Transform sphere;

        private IEnumerator SphereScale()
        {
            for (int i = 0; i < 10; i++) {
                sphere.localScale += Vector3.one;
                yield return new WaitForSeconds(0.2f);
            }

        }

        private void Start()
        {
            //啟動協同程序
            StartCoroutine(TestCoroutine());
            StartCoroutine(SphereScale());
        }
    }
}


