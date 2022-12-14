using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class csInGameUiManager : MonoBehaviour
{
    //  Esc
    public GameObject Info;  // Esc - Pop의 Info

    // Inven
    public GameObject Inven_Item; // 탈출 아이템 인벤 페이지 (Inven - Pop)
    public GameObject Inven_CluePage;  // 단서 페이지 (Inven - Pop)
    //public GameObject Inven_Special;  // 특수 아이템 인벤 페이지 (Inven - Pop)
    //단서 프리팹
    public GameObject Inven_ClueList; //단서들 리스트 불러와지는 프리팹, ClueList_Prf 으로 태그달았음
    public GameObject Inven_Clue; //단서 프리팹, CluePrf 으로 태그 달았음
    public GameObject scrollContents; // 단서리스트랑 프리팹 생길 곳

    // Map
    public GameObject M1;  //1층
    public GameObject M2;  //2층
    public GameObject M3;  //3층

    // 튜토리얼
    public GameObject tutorialBtn;
    public Sprite[] tutorial_popup;   // 튜토리얼 이미지 4장
    public Image tutorial;       // 하이라키의 Tutorial, 여기에 이미지 들어가야함
    private int t_count = 0;

    public GameObject InGameUi; // 모든 InGameUi의 부모
    public GameObject[] Btns;  // InGameUi 의 버튼들

    //인벤토리 구현
    public Inventory theInventory;
    [SerializeField]
    private GameObject go_InventoryBase;  // 인벤토리 Page Onoff 시키기 위함... 부모로 하이라키에 만들어놓음
    public bool inventoryActivated = false; // 인벤 켜지면 카메라나 다른 것 기능 안하게... 안움직이게 막는것
    [SerializeField]
    private GameObject MainMenu;
    public bool MainMenuActivated = false;
    [SerializeField]
    private GameObject Map;
    public bool MapActivated = false;

    // Esc
    public void OnClickInfo()
    {
        Info.SetActive(true);
    }

    public bool InventoryOnOff()
    {
        if (inventoryActivated)
        {
            CloseInventory();
            //CloseMainMenu();
            //CloseMap();
            return false;
        }
        else
        {
            CloseMainMenu();
            CloseMap();
            OpenInventory();
            return true;
        }
        inventoryActivated = !inventoryActivated;
    }
    public bool MainMenuOnOff()
    {
        if (MainMenuActivated)
        {
            CloseMainMenu();
            //CloseMainMenu();
            //CloseMap();
            return false;
        }
        else
        {
            OpenMainMenu();
            CloseMap();
            CloseInventory();
            return true;
        }
        MainMenuActivated = !MainMenuActivated;
    }
    public bool MapOnOff()
    {
        if (MapActivated)
        {
            CloseMap();
            //CloseMainMenu();
            //CloseMap();
            return false;
        }
        else
        {
            CloseMainMenu();
            OpenMap();
            CloseInventory();
            return true;
        }
        MapActivated = !MapActivated;
    }
    private void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        MainMenuActivated = true;
    }
    private void CloseMainMenu()
    {
        MainMenu.SetActive(false);
        MainMenuActivated = false;
    }
    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        inventoryActivated = true;
    }
    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        inventoryActivated = false;
    }
    private void OpenMap()
    {
        Map.SetActive(true);
        MapActivated = true;
    }
    private void CloseMap()
    {
        Map.SetActive(false);
        MapActivated = false;
    }
    public void OnClickInven_Item()
    {
        Inven_CluePage.SetActive(false);
        //Inven_Special.SetActive(false);

        Inven_Item.SetActive(true);
    }
    public void OnClickInven_Clue()  // 단서 Btn 클릭하면 Scroll View (단서 리스트들이 뜰 곳) Popup 
    {
        Inven_Item.SetActive(false);
        //Inven_Special.SetActive(false);

        Inven_CluePage.SetActive(true); //  scroll view 가 popup 되고
    }

    public void OnClick_X() //단서 PopUp된것 닫기
    {
        Inven_Clue.SetActive(false);
        Inven_ClueList.SetActive(true);

    }

    public void OnClickInven_Special()
    {
        Inven_Item.SetActive(false);
        Inven_CluePage.SetActive(false);

        //Inven_Special.SetActive(true);
    }
    public void OnClick_OpenTutorial()
    {
        tutorialBtn.SetActive(true);
    }

    public void OnClick_CloseTutorial()
    {
        tutorialBtn.SetActive(false);
    }

    public void OnClick_Tutorial_Right() // 단서에 기본으로 들어있는 튜토리얼 왼쪽으로 넘기기
    {
        t_count += 1;

        tutorial.sprite = tutorial_popup[t_count % 4];

    }

    public void OnClick_Tutorial_Left() // 단서에 기본으로 들어있는 튜토리얼 오른쪽으로 넘기기

    {
        t_count -= 1;

        if (t_count < 0)
        {
            t_count = 3;
        }

        tutorial.sprite = tutorial_popup[t_count % 4];
    }

    //Map
    public void OnClick_M1()
    {
        M2.SetActive(false);
        M3.SetActive(false);

        M1.SetActive(true);
    }
    public void OnClick_M2()
    {
        M1.SetActive(false);
        M3.SetActive(false);

        M2.SetActive(true);
    }
    public void OnClick_M3()
    {
        M1.SetActive(false);
        M2.SetActive(false);

        M3.SetActive(true);
    }

    //ReturnBtn
    public void OnClick_InGame_ReturnBtn() //InGame Ui에서 나와서 다시 게임으로 돌아감, ReturnBtn
    {
        Map.SetActive(false);
        MainMenu.SetActive(false);
        go_InventoryBase.SetActive(false);
        MainMenuActivated = false;
        inventoryActivated = false;
        MapActivated = false;
        //구조상 문제로 무식하게 진행.. 리턴 버튼 클릭시 Player의 isStop false로 변경 및 커서보임.
        GameObject myPlayer = GameObject.Find("Player_FP");
        if (myPlayer != null)
        {
            myPlayer.transform.root.GetComponent<csPlayerCtrl>().isStop = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    //ExitBtn 
    public void OnClickReturnLobby()// InGame에서 Lobby로 나감. LeaveRoom()- 네트워크 Room에서 나가고, LogMsg로 나간 PlayerId 
    {
        SceneManager.LoadScene("scNetLobby");
        PhotonNetwork.LeaveRoom();
    }

}