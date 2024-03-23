using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialModalManager : MonoBehaviour
{
    public static TutorialModalManager Instance;

    public GameObject canvas;
    public GameObject container;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text pageCountDisplay;

    public GameObject leftButton;
    public TMP_Text leftButtonText;
    public TMP_Text rightButtonText;

    public TutorialCartData tutorialCartDataPrefab;

    public int pageCount;

    private int currentPage;

    void Start()
    {
        pageCount = 0;
        currentPage = 1;
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RenderCartTutorial()
    {
        currentPage = 1;
        pageCount = 3;
        title.SetText("Carts");
        canvas.SetActive(true);

        description.SetText(string.Format("After reaching a cart node an empty cart will appear on your train.\n\nYour carts can be managed <size=6><b>[but not moved]</b></size> by clicking on them. The type can always change by choosing the desired cart type and by paying it's respective manufacture cost.\n\n\n<align=\"center\"><size=13><color=#B5AFFF><b>careful!</b></color></size>\n<size=11>Carts will use their manufacture costs <color=#B5AFFF><b>instantly</b></color>,\nspamming will deplete your suplies.</size></align>\n\n\nYour carts require <sprite=3> to function, they waste <color=#B5AFFF><b>{0:N2}<sprite=3><size=7>/s</size></b></color> even when disabled due to their weight.\n\nCarts will also slow down your train by <color=#B5AFFF><b>{1:N0}</b></color><sprite=0> for every <color=#B5AFFF><b>{2:N0}</b></color><sprite=4>\n\nfarms, reactors and extra engines require <color=#B5AFFF><b>workers</b></color><sprite=1> to operate them.", TrainManager.Instance.cartsEnergyConsumption, TrainManager.Instance.slowdownAmount, TrainManager.Instance.cartsToSlowdown));
    }
    public void RenderCartTutorialPage(int page)
    {
        if (page == 1)
        {
            RenderCartTutorial();
            return;
        }
        currentPage = page;
        string information;
        string manufacture;
        string manufactureModifiers;
        if (page == 2)
        {
            description.SetText("");
            information = string.Format("Farms produce <sprite=2>\nThey require at least <color=#B5AFFF><b>1</b></color><sprite=1> worker, and for each worker outputs<color=#B5AFFF> <b>{0:N2}<sprite=2><size=7>/s</size></b></color>\nat <color=#B5AFFF><b>Max<size=7>[{1:N0}]</size></b></color> workers produce an extra <color=#B5AFFF><b>{2:N2}<sprite=2><size=7>/s</size></b></color>\nin total <color=#B5AFFF><b>{3:N2}<sprite=2><size=7>/s</size></b></color>\nconsuming <color=#B5AFFF><b>{4:N2}<sprite=3><size=7>/s</size></b></color>", TrainManager.Instance.farmOutput, TrainManager.Instance.farmWorkersCapacityPerCart, TrainManager.Instance.farmValueAtMaxWorkers, TrainManager.Instance.farmOutput * TrainManager.Instance.farmWorkersCapacityPerCart + TrainManager.Instance.farmValueAtMaxWorkers, TrainManager.Instance.farmFoodProductionEnergyCost + TrainManager.Instance.cartsEnergyConsumption);
            manufacture = string.Format("\n\nbase manufacture cost <color=#B5AFFF> <b>{0:N0}<sprite=2>  {1:N2}<sprite=3>  {2:N0}<size=6>s</size></b></color>", TrainManager.Instance.farmManufactureFoodCost, TrainManager.Instance.farmManufactureEnergyCost, 10);
            manufactureModifiers = string.Format("\n\ncost increase by farm created <color=#B5AFFF> <b>{0:N0}<sprite=2>  {1:N2}<sprite=3></b></color>", 0, TrainManager.Instance.farmManufactureCostIncrease);
            tutorialCartDataPrefab.SetData(CartType.FARM, information + manufacture + manufactureModifiers);
            Instantiate(tutorialCartDataPrefab, container.transform);

            information = string.Format("Increases the housing capacity by <color=#B5AFFF><b>{0:N0}</b></color><sprite=1>\nPassengers require housing to work, they won't work if there isn't enough houses available\nconsuming <color=#B5AFFF><b>{1:N2}<sprite=3><size=7>/s</size></b></color>", 5, TrainManager.Instance.cartsEnergyConsumption);
            manufacture = string.Format("\n\nbase manufacture cost <color=#B5AFFF> <b>{0:N0}<sprite=2>  {1:N2}<sprite=3>  {2:N0}<size=6>s</size></b></color>", TrainManager.Instance.housingManufactureFoodCost, TrainManager.Instance.housingManufactureEnergyCost, 5);
            manufactureModifiers = string.Format("\n\ncost increase by housing created <color=#B5AFFF> <b>{0:N2}<sprite=2><sprite=3></b></color>", TrainManager.Instance.housingManufactureCostIncrease);
            tutorialCartDataPrefab.SetData(CartType.HOUSING, information + manufacture + manufactureModifiers);
            Instantiate(tutorialCartDataPrefab, container.transform);

            information = string.Format("Require <color=#B5AFFF><b>{0:N0}</b></color><sprite=1> to produce extra <sprite=3>\nconsuming <color=#B5AFFF><b>-{1:N2}<sprite=3><size=7>/s</size></b></color>\nproducing <color=#B5AFFF><b>{2:N2}<sprite=3><size=7>/s</size></b></color>\nin total <color=#B5AFFF><b>{3:N2}<sprite=3><size=7>/s</size></b></color>", TrainManager.Instance.reactorWorkersRequiredPerCart, TrainManager.Instance.cartsEnergyConsumption, TrainManager.Instance.reactorOutput, TrainManager.Instance.reactorOutput - TrainManager.Instance.cartsEnergyConsumption);
            manufacture = string.Format("\n\nbase manufacture cost <color=#B5AFFF> <b>{0:N0}<sprite=2><sprite=3>  {2:N0}<size=6>s</size></b></color>", TrainManager.Instance.reactorManufactureFoodCost, TrainManager.Instance.reactorManufactureEnergyCost, 30);
            manufactureModifiers = string.Format("\n\ncost increase by reactor created <color=#B5AFFF> <b>{0:N2}<sprite=2><sprite=3></b></color>", TrainManager.Instance.reactorManufactureCostIncrease);
            tutorialCartDataPrefab.SetData(CartType.REACTOR, information + manufacture + manufactureModifiers);
            Instantiate(tutorialCartDataPrefab, container.transform);
        }

        if (page == 3)
        {
            information = string.Format("Require <color=#B5AFFF><b>{0:N0}</b></color><sprite=1> to produce extra <sprite=0>\nconsuming <color=#B5AFFF><b>{1:N2}<sprite=3><size=7>/s</size></b></color>\nproducing <color=#B5AFFF><b>{2:N2}<sprite=0></b></color>\nspeed to energy convertion <color=#B5AFFF><b>{2:N2}<sprite=3>/<sprite=0></b></color>", 5, TrainManager.Instance.extraEngineWorkersRequiredPerCart, TrainManager.Instance.cartsEnergyConsumption, TrainManager.Instance.extraEngineSpeedOutput, TrainManager.Instance.kwPerKmPerS);
            manufacture = string.Format("\n\nbase manufacture cost <color=#B5AFFF> <b>{0:N0}<sprite=2>  {1:N2}<sprite=3>  {2:N0}<size=6>s</size></b></color>", TrainManager.Instance.extraEngineManufactureFoodCost, TrainManager.Instance.extraEngineManufactureEnergyCost, 180);
            manufactureModifiers = string.Format("\n\ncost increase by extra engine created <color=#B5AFFF> <b>{0:N0}<sprite=2><sprite=3></b></color>", TrainManager.Instance.extraEngineManufactureCostIncrease);
            tutorialCartDataPrefab.SetData(CartType.EXTRA_ENGINE, information + manufacture + manufactureModifiers);
            Instantiate(tutorialCartDataPrefab, container.transform);

            information = string.Format("Increase energy capacity by <color=#B5AFFF><b>{0:N0}</b></color><sprite=3>\nCounts as <color=#B5AFFF><b>{1:N0}</b></color><sprite=4>\nconsuming <color=#B5AFFF><b>{2:N2}<sprite=3><size=7>/s</size></b></color>", TrainManager.Instance.batteryModifier, 2, TrainManager.Instance.cartsEnergyConsumption * 2);
            manufacture = string.Format("\n\nbase manufacture cost <color=#B5AFFF><b>{0:N0}<sprite=2>  {1:N2}<sprite=3>  {2:N0}<size=6>s</size></b></color>", TrainManager.Instance.batteryManufactureFoodCost, TrainManager.Instance.batteryManufactureEnergyCost, 80);
            manufactureModifiers = string.Format("\n\ncost increase by battery created <color=#B5AFFF> <b>{0:N2}<sprite=2><sprite=3></b></color>", TrainManager.Instance.batteryManufactureCostIncrease);
            tutorialCartDataPrefab.SetData(CartType.BATTERY, information + manufacture + manufactureModifiers);
            Instantiate(tutorialCartDataPrefab, container.transform);
        }
    }

    public void RenderDestinationTutorial()
    {
        currentPage = 1;
        pageCount = 1;
        title.SetText("Destinations");
        canvas.SetActive(true);

        description.SetText("Congratulations on reaching your first destination! But what are those you ask yourself? Destinations are your main goal!\n\nReaching them will reward you with a range of resources, and will also display a new set of destinations <size=8><b>[from 2 up to 3]</b></size>.\n\nSome can be more challenging than others offering some debuffs along the way, and others may reward you if you choose them.\n\nA few routes will include weather modifiers which control the ouput of your productions [<sprite=3>|<sprite=2>] in positive or negative ways.\n\n<b>Be careful</b> when choosing your next destination! Some routes may be more punishing then others!\n\n<b>Some rewards may be</b>\n\n<color=#B5AFFF><b>extra/max</b></color><sprite=3>\n\n<color=#B5AFFF><b>extra/max</b></color><sprite=2>\n\n<color=#B5AFFF><b>Passengers</b></color><sprite=1>\n<color=#B5AFFF><b>carts</b></color><sprite=4>\n<color=#B5AFFF><b>special carts</b></color><sprite=4>");
    }

    public void LeftButtonClick()
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        if (currentPage > 1 && pageCount != 1)
        {
            currentPage--;
            if (title.text == "Carts")
            {
                RenderCartTutorialPage(currentPage);
            }
        }
        else
        {
            // TrainManager.Instance.Play();
            canvas.SetActive(false);
        }
    }

    public void RightButtonClick()
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }
        if (currentPage < pageCount && pageCount != 1)
        {
            currentPage++;
            if (title.text == "Carts")
            {
                RenderCartTutorialPage(currentPage);
            }
        }
        else
        {
            // TrainManager.Instance.Play();
            canvas.SetActive(false);
        }
    }

    void Update()
    {
        pageCountDisplay.SetText(string.Format("{0:0}/{1:0}", currentPage, pageCount));
        if (pageCount == 1)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }
        if (currentPage == 1 && pageCount != 1)
        {

            leftButtonText.SetText("Close");
        }
        else
        {

            leftButtonText.SetText("Previous");
        }

        if (currentPage == pageCount)
        {
            rightButtonText.SetText("Done");
        }
        else
        {

            rightButtonText.SetText("Next");
        }

    }
}
