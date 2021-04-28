using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Student_level_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;

    private void Awake(){
        graphContainer= transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

        //CreateCircle( new Vector2(200, 200));
        List<int> valueList = new List<int>(){ 5,7, 3, 16, 18, 45, 4, 6};
        List<string> levelList = new List<string>(){"Conversion","Proportion", "Ratio", "Measuring", "Division", "Percentages", "Average ", "Estimation"};
        showGraph(valueList,levelList);
    }

    private void CreateCircle(Vector2 anchoredPosition){

        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
    }

    private void showGraph(List<int> valueList, List<string> levelList){
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f; // y axis is how many students attempted each level (iterated)
        float xSize =100f; // x axis is level number
        
        for (int i=0; i<valueList.Count; i++){
            float xPosition =  xSize+ i * xSize;
            float yPosition = (valueList[i]/yMaximum)* graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition));

            RectTransform labelX= Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            labelX. GetComponent<Text>().text = levelList[i];



        }
        int seperatorCount = levelList.Count;
        for (int i = 0; i< seperatorCount; i++){
            RectTransform labelY= Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i* 1f/ seperatorCount; 
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue *graphHeight );
            labelY. GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
        }


          

    }
     private GameObject CreateBar( Vector2 graphPosition, float barWidth  ) {
        GameObject gameObject = new GameObject("bar ", typeof(Image));
        gameObject.transform.SetParent(graphContainer,false);  
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = graphPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        rectTransform.pivot = new Vector2(.5f, 0f);
        return gameObject;
           
     }

}

