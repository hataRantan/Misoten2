//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//public class MyItemAppearTest : MonoBehaviour
//{
//    [SerializeField]
//    TextMeshProUGUI s = null;

//    [SerializeField]
//    TextMeshProUGUI a = null;

//    [SerializeField]
//    TextMeshProUGUI b = null;

//    [SerializeField]
//    TextMeshProUGUI c = null;

//    [SerializeField]
//    TextMeshProUGUI d = null;

//    int sCnt = 0;
//    int aCnt = 0;
//    int bCnt = 0;
//    int cCnt = 0;
//    int dCnt = 0;

//    public void ContItem(MyItemManager.Rank _rank)
//    {
//        switch (_rank)
//        {
//            case MyItemManager.Rank.S:
//                sCnt++;
//                s.text = sCnt.ToString();
//                break;

//            case MyItemManager.Rank.A:
//                aCnt++;
//                a.text = aCnt.ToString();
//                break;

//            case MyItemManager.Rank.B:
//                bCnt++;
//                b.text = bCnt.ToString();
//                break;

//            case MyItemManager.Rank.C:
//                cCnt++;
//                c.text = cCnt.ToString();
//                break;

//            case MyItemManager.Rank.D:
//                dCnt++;
//                d.text = dCnt.ToString();
//                break;
//        }

//    }

//}
