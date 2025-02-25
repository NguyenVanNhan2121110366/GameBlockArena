using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotInteraction : MonoBehaviour
{
    #region Variable
    private GridController grid;
    [SerializeField] private bool isDragging;
    [SerializeField] private int column, row;
    [SerializeField] private bool isMatched;
    private GameObject downDotObj, upDotObj, leftleftDot,
    rightrightDot, leftDot, rightDot, downdownDot, upupDot
    , leftUpDot, rightUpDot, downLeftRight, downRightLeft;
    private int countFindMatched;
    private GridController allDots;
    #endregion
    #region Public
    public int Column { get => column; set => column = value; }
    public int Row { get => row; set => row = value; }
    public bool IsMatched { get => isMatched; set => isMatched = value; }

    #endregion
    private void Awake()
    {
        if (grid == null) grid = FindFirstObjectByType<GridController>();
        else Debug.Log("grid was exist");
        this.allDots = FindFirstObjectByType<GridController>();
    }
    private void Start()
    {
        rightUpDot = null; leftUpDot = null; upupDot = null;
        downdownDot = null; downDotObj = null; upDotObj = null;
        leftDot = null; rightDot = null; rightrightDot = null;
        leftleftDot = null; downLeftRight = null; downRightLeft = null;
        this.countFindMatched = 0;
        this.isMatched = false;
    }

    private void Update()
    {
        this.Find3Matched();
    }

    #region Destroy
    private IEnumerator CountFindMatched(int diamonPlus, float plusX, float plusY)
    {
        countFindMatched++;
        if (countFindMatched == 1)
        {
            yield return new WaitForSeconds(0.3f);
            //countFindMatched = 0;
            ScoreUIPlayGame.Instance.MovePlusDiamon(diamonPlus, plusX, plusY);
        }
    }

    private void CheckAllDot()
    {


        //Check left dot

        //Check right dot


        if (this.allDots.LastObj)
        {
            //Check down left dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column - 1 >= 0
            && this.allDots.LastObj.GetComponent<DotInteraction>().Row - 1 >= 0)
            {
                downLeftRight = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column - 1,
                this.allDots.LastObj.GetComponent<DotInteraction>().Row - 1];
            }

            //Check down right dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column + 1 < this.allDots.With
            && this.allDots.LastObj.GetComponent<DotInteraction>().Row - 1 >= 0)
            {
                downRightLeft = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column + 1,
                this.allDots.LastObj.GetComponent<DotInteraction>().Row - 1];
            }

            //Check right up dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column + 1 < this.allDots.With
            && this.allDots.LastObj.GetComponent<DotInteraction>().Row + 1 < this.allDots.Height)
            {
                rightUpDot = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column + 1,
                this.allDots.LastObj.GetComponent<DotInteraction>().Row + 1];
            }
            //Check left up dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column - 1 >= 0
            && this.allDots.LastObj.GetComponent<DotInteraction>().Row + 1 < this.allDots.Height)
            {
                leftUpDot = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column - 1,
                this.allDots.LastObj.GetComponent<DotInteraction>().Row + 1];
            }
            //Check left dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column - 1 >= 0)
            {
                leftDot = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column - 1, row];
            }
            //Check right dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column + 1 < this.allDots.With)
            {
                rightDot = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column + 1, row];
            }

            //Check down dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Row - 1 >= 0)
            {
                downDotObj = this.allDots.AllDots[column, this.allDots.LastObj.GetComponent<DotInteraction>().Row - 1];
            }

            //Check down down dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Row - 2 == 0)
            {
                downdownDot = this.allDots.AllDots[column, this.allDots.LastObj.GetComponent<DotInteraction>().Row - 2];
            }

            //Check up up dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Row + 2 < this.allDots.Height)
            {
                upupDot = this.allDots.AllDots[column, this.allDots.LastObj.GetComponent<DotInteraction>().Row + 2];
            }

            //-------------------------------------------------------------------------------------
            //Check up dot

            if (this.allDots.LastObj.GetComponent<DotInteraction>().Row + 1 < this.allDots.Height)
            {
                upDotObj = this.allDots.AllDots[column, this.allDots.LastObj.GetComponent<DotInteraction>().Row + 1];
            }

            //Check left left dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column - 2 == 0)
            {
                leftleftDot = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column - 2, row];
            }

            //Check right right dot
            if (this.allDots.LastObj.GetComponent<DotInteraction>().Column + 2 < this.allDots.With && !downdownDot)
            {
                rightrightDot = this.allDots.AllDots[this.allDots.LastObj.GetComponent<DotInteraction>().Column + 2, row];
            }
        }
    }
    private void Find3Matched()
    {
        this.CheckAllDot();
        //Check horizontal
        if (column < this.allDots.With - 1 && column > 0)
        {
            var leftDot = this.allDots.AllDots[column - 1, row];
            var rightDot = this.allDots.AllDots[column + 1, row];
            if (rightDot && leftDot && rightDot.CompareTag(gameObject.tag) && leftDot.CompareTag(gameObject.tag))
            {
                if (rightDot != gameObject && rightDot != leftDot && leftDot != gameObject)
                {
                    rightDot.GetComponent<DotInteraction>().IsMatched = true;
                    leftDot.GetComponent<DotInteraction>().IsMatched = true;
                    isMatched = true;
                    this.allDots.CheckDestroyList = false;
                    GameStateController.Instance.CurrentGameState = GameState.Checking;
                    Debug.Log("Horizontal");
                }
            }
        }


        //Check vertical
        if (row + 1 < this.allDots.Height && row - 1 >= 0)
        {
            var upDot = this.allDots.AllDots[column, row + 1];
            var downDot = this.allDots.AllDots[column, row - 1];
            if (upDot && downDot && upDot.CompareTag(gameObject.tag) && downDot.CompareTag(gameObject.tag))
            {
                if (upDot != downDot && upDot != gameObject && downDot != gameObject)
                {
                    upDot.GetComponent<DotInteraction>().IsMatched = true;
                    downDot.GetComponent<DotInteraction>().IsMatched = true;
                    this.isMatched = true;
                    this.allDots.CheckDestroyList = false;
                    GameStateController.Instance.CurrentGameState = GameState.Checking;
                    Debug.Log("Check Vertical");
                }
            }
        }

        //Check Up left
        if (column - 1 >= 0 && row + 1 < this.allDots.Height)
        {
            var leftDot = this.allDots.AllDots[column - 1, row];
            var upDot = this.allDots.AllDots[column, row + 1];
            if (leftDot && upDot && leftDot.CompareTag(this.gameObject.tag) && upDot.CompareTag(this.gameObject.tag))
            {
                leftDot.GetComponent<DotInteraction>().IsMatched = true;
                upDot.GetComponent<DotInteraction>().IsMatched = true;
                isMatched = true;
                this.allDots.CheckDestroyList = false;
                GameStateController.Instance.CurrentGameState = GameState.Checking;
            }
        }
        //Check Up right
        if (column + 1 < this.allDots.With && row + 1 < this.allDots.Height)
        {
            var upDot = this.allDots.AllDots[column, row + 1];
            var rightDot = this.allDots.AllDots[column + 1, row];
            var originDot = this.allDots.AllDots[column, row];
            if (originDot && rightDot && upDot && rightDot.CompareTag(this.gameObject.tag) && upDot.CompareTag(this.gameObject.tag))
            {
                isMatched = true;
                rightDot.GetComponent<DotInteraction>().IsMatched = true;
                upDot.GetComponent<DotInteraction>().IsMatched = true;
                this.allDots.CheckDestroyList = false;
                GameStateController.Instance.CurrentGameState = GameState.Checking;
            }
        }

        //check Down left
        if (row - 1 >= 0 && column - 1 >= 0)
        {
            var downDot = this.allDots.AllDots[column, row - 1];
            var leftDot = this.allDots.AllDots[column - 1, row];
            if (downDot && leftDot && leftDot.CompareTag(gameObject.tag) && downDot.CompareTag(this.gameObject.tag))
            {
                downDot.GetComponent<DotInteraction>().IsMatched = true;
                leftDot.GetComponent<DotInteraction>().IsMatched = true;
                isMatched = true;
                this.allDots.CheckDestroyList = false;
                GameStateController.Instance.CurrentGameState = GameState.Checking;
            }
        }
        //check Down right
        if (row - 1 >= 0 && column + 1 < this.allDots.Height)
        {
            var downDot = this.allDots.AllDots[column, row - 1];
            var rightDot = this.allDots.AllDots[column + 1, row];
            if (downDot && rightDot && rightDot.CompareTag(gameObject.tag) && downDot.CompareTag(this.gameObject.tag))
            {
                downDot.GetComponent<DotInteraction>().IsMatched = true;
                rightDot.GetComponent<DotInteraction>().IsMatched = true;
                isMatched = true;
                this.allDots.CheckDestroyList = false;
                GameStateController.Instance.CurrentGameState = GameState.Checking;
            }
        }
        this.CheckFind4Matched();
    }


    // You need check this function tomorrow
    private void CheckFind4Matched()
    {

        // Horizontal right right left
        if (column + 2 < this.allDots.With && column - 1 == 0)
        {
            var rightDotObj = this.allDots.AllDots[column + 1, row];
            var leftDotObj = this.allDots.AllDots[column - 1, row];

            if (!leftleftDot && rightrightDot && leftDot && rightDot && rightrightDot.CompareTag(rightDot.tag)
            && rightDot.CompareTag(leftDot.tag) && leftDot.CompareTag(gameObject.tag))
            {
                if (!downDotObj && !upDotObj || downDotObj && !downDotObj.CompareTag(gameObject.tag)
                || upDotObj && !upDotObj.CompareTag(gameObject.tag))
                {
                    {
                        StartCoroutine(this.CountFindMatched(5, 0.3f, 0));
                        Debug.Log("Destroy right dot");
                    }
                }
                ////Dang bi loi o day
            }

        }

        //left left right
        if (column - 2 == 0 && column + 1 < this.allDots.With)
        {
            var rightDotObj = this.allDots.AllDots[column + 1, row];
            var leftDotObj = this.allDots.AllDots[column - 1, row];
            if (leftleftDot && leftDotObj && rightDotObj && leftleftDot.CompareTag(rightDotObj.tag)
            && rightDotObj.CompareTag(gameObject.tag) && leftDotObj.CompareTag(gameObject.tag))
            {
                //.........
                rightrightDot = null;
                if (!rightrightDot && !downDotObj && !upDotObj || !upDotObj && downDotObj && !downDotObj.CompareTag(gameObject.tag)
                || !downDotObj && upDotObj && !upDotObj.CompareTag(gameObject.tag))
                {
                    if (rightDotObj != leftDotObj && rightDotObj != gameObject && leftDotObj != gameObject)
                    {
                        leftleftDot.GetComponent<DotInteraction>().IsMatched = true;
                        StartCoroutine(this.CountFindMatched(5, 0.3f, 0));
                        Debug.Log("Destroy left dot");
                    }
                }
            }
        }

        // up right right left
        if (column - 1 == 0 && column + 2 < this.allDots.With && row + 1 < this.allDots.Height)
        {
            var rightDot = this.allDots.AllDots[column + 1, row];
            var leftDot = this.allDots.AllDots[column - 1, row];
            // var upDot = this.allDots.AllDots[column, row + 1];
            // var downDot = this.allDots.AllDots[column, row - 1];
            if (rightrightDot && rightDot && upDotObj && leftDot &&
            rightrightDot.CompareTag(rightDot.tag) && rightDot.CompareTag(upDotObj.tag) && upDotObj.CompareTag(leftDot.tag))
            {
                if (!downDotObj || downDotObj && downDotObj.CompareTag(gameObject.tag))
                {
                    if (rightDot != gameObject && leftDot != gameObject && rightDot != leftDot)
                    {
                        rightrightDot.GetComponent<DotInteraction>().IsMatched = true;
                        StartCoroutine(this.CountFindMatched(10, 0.3f, 0));
                        Debug.Log("Destroy right 5 dot up");
                    }
                }
            }
        }

        //down right right left
        if (column - 1 == 0 && column + 2 < this.allDots.With && row - 1 >= 0)
        {
            var rightDot = this.allDots.AllDots[column + 1, row];
            var leftDot = this.allDots.AllDots[column - 1, row];
            // var upDot = this.allDots.AllDots[column, row + 1];
            // var downDot = this.allDots.AllDots[column, row - 1];
            if (rightrightDot && rightDot && downDotObj && leftDot &&
            rightrightDot.CompareTag(rightDot.tag) && rightDot.CompareTag(downDotObj.tag) && downDotObj.CompareTag(leftDot.tag))
            {
                if (!downDotObj || downDotObj && downDotObj.CompareTag(gameObject.tag))
                {
                    if (rightDot != gameObject && leftDot != gameObject && rightDot != leftDot)
                    {
                        rightrightDot.GetComponent<DotInteraction>().IsMatched = true;
                        StartCoroutine(this.CountFindMatched(10, 0.3f, 0));
                        Debug.Log("Destroy right 5 dot down");
                    }
                }
            }
        }

        // up left left right
        if (column - 2 == 0 && column + 1 < this.allDots.With && row + 1 < this.allDots.Height)
        {
            var rightDot = this.allDots.AllDots[column + 1, row];
            var leftDot = this.allDots.AllDots[column - 1, row];
            // var upDot = this.allDots.AllDots[column, row + 1];
            // var downDot = this.allDots.AllDots[column, row - 1];
            if (leftleftDot && rightDot && upDotObj && leftDot &&
            leftleftDot.CompareTag(rightDot.tag) && rightDot.CompareTag(upDotObj.tag) && upDotObj.CompareTag(leftDot.tag))
            {
                if (!downDotObj || downDotObj && !downDotObj.CompareTag(gameObject.tag))
                {
                    if (rightDot != gameObject && leftDot != gameObject && rightDot != leftDot)
                    {
                        leftleftDot.GetComponent<DotInteraction>().IsMatched = true;
                        StartCoroutine(this.CountFindMatched(10, 0.3f, 0));
                        Debug.Log("Destroy left 5 dot up");
                    }
                }

            }
        }

        // down left left right
        if (column - 2 == 0 && column + 1 < this.allDots.With && row + 1 < this.allDots.Height)
        {
            var rightDot = this.allDots.AllDots[column + 1, row];
            var leftDot = this.allDots.AllDots[column - 1, row];
            // var upDot = this.allDots.AllDots[column, row + 1];
            // var downDot = this.allDots.AllDots[column, row - 1];
            if (leftleftDot && rightDot && downDotObj && leftDot &&
            leftleftDot.CompareTag(rightDot.tag) && rightDot.CompareTag(downDotObj.tag) && downDotObj.CompareTag(leftDot.tag))
            {
                if (!upDotObj || upDotObj && upDotObj.CompareTag(gameObject.tag))
                {
                    if (rightDot != gameObject && leftDot != gameObject && rightDot != leftDot)
                    {
                        leftleftDot.GetComponent<DotInteraction>().IsMatched = true;
                        StartCoroutine(this.CountFindMatched(10, 0.3f, 0));
                        Debug.Log("Destroy left 5 dot up");
                    }
                }
            }
        }

        //------------------------------Up Down--------------------------------
        if (row > 0 && row < this.allDots.Height - 2)
        {
            if (!downdownDot && upDotObj && downDotObj && upupDot && upDotObj.CompareTag(gameObject.tag)
            && downDotObj.CompareTag(gameObject.tag) && upupDot.CompareTag(gameObject.tag))
            {
                if (!leftDot && !rightDot || leftDot && !rightDot && !leftDot.CompareTag(gameObject.tag)
                || rightDot && !leftDot && !rightDot.CompareTag(gameObject.tag))
                {
                    StartCoroutine(this.CountFindMatched(5, 0, 0.3f));
                    Debug.Log("Destroy up up down dot");
                }
            }
        }

        // down down up dot
        if (row - 2 == 0 && row + 1 < this.allDots.Height)
        {
            if (!upupDot && downdownDot && downDotObj && upDotObj && downdownDot.CompareTag(downDotObj.tag)
            && downDotObj.CompareTag(upDotObj.tag) && upDotObj.CompareTag(gameObject.tag))
            {
                if (!leftDot && !rightDot || leftDot && !leftDot.CompareTag(gameObject.tag)
                || rightDot && !rightDot.CompareTag(gameObject.tag))
                {
                    StartCoroutine(this.CountFindMatched(5, 0, 0.3f));
                    Debug.Log("Destroy down down up dot");
                }
            }
        }

        //up down down right

        if (row - 2 == 0 && row + 1 < this.allDots.Height && column + 1 < this.allDots.With)
        {
            if (rightDot && !upupDot && downdownDot && downDotObj && upDotObj && downdownDot.CompareTag(downDotObj.tag)
            && downDotObj.CompareTag(upDotObj.tag) && upDotObj.CompareTag(rightDot.tag)
            && rightDot.CompareTag(gameObject.tag))
            {
                StartCoroutine(this.CountFindMatched(10, 0, 0.3f));
                Debug.Log("Destroy down down up right dot");
            }
        }

        //up down down left

        if (row - 2 == 0 && row + 1 < this.allDots.Height && column - 1 >= 0)
        {
            if (leftDot && !upupDot && downdownDot && downDotObj && upDotObj && downdownDot.CompareTag(downDotObj.tag)
            && downDotObj.CompareTag(upDotObj.tag) && upDotObj.CompareTag(leftDot.tag)
            && leftDot.CompareTag(gameObject.tag))
            {
                StartCoroutine(this.CountFindMatched(10, 0, 0.3f));
                Debug.Log("Destroy down down up left dot");
            }
        }

        //up up down right

        if (row + 2 < this.allDots.Height && row - 1 == 0 && column + 1 < this.allDots.With)
        {
            if (!leftDot && rightDot && !downdownDot && upupDot && downDotObj && upDotObj && upupDot.CompareTag(downDotObj.tag)
            && downDotObj.CompareTag(upDotObj.tag) && upDotObj.CompareTag(rightDot.tag)
            && rightDot.CompareTag(gameObject.tag))
            {
                StartCoroutine(this.CountFindMatched(10, 0, 0.3f));
                Debug.Log("Destroy up up down right dot");
            }
        }

        //up up down left

        if (row + 2 < this.allDots.Height && row - 1 == 0 && column - 1 >= 0)
        {
            if (!rightDot && leftDot && !downdownDot && upupDot && downDotObj && upDotObj && upupDot.CompareTag(downDotObj.tag)
            && downDotObj.CompareTag(upDotObj.tag) && upDotObj.CompareTag(leftDot.tag)
            && leftDot.CompareTag(gameObject.tag))
            {
                StartCoroutine(this.CountFindMatched(10, 0, 0.3f));
                Debug.Log("Destroy up up down left dot");
            }
        }

        //up left right
        if (column + 1 < this.allDots.With && column - 1 >= 0 && row + 1 < this.allDots.Height)
        {
            if (leftDot && rightDot && leftUpDot && leftUpDot.CompareTag(leftDot.tag)
            && leftDot.CompareTag(rightDot.tag) && rightDot.CompareTag(gameObject.tag))
            {
                if (gameObject != leftDot && gameObject != rightDot && leftDot != rightDot)
                {
                    StartCoroutine(this.CountFindMatched(5, 0.3f, 0));
                    Debug.Log("Destroy up left right");
                }

            }
        }

        //up right left
        if (column + 1 < this.allDots.With && column - 1 >= 0 && row + 1 < this.allDots.Height)
        {
            if (leftDot && rightDot && rightUpDot && rightUpDot.CompareTag(leftDot.tag)
            && leftDot.CompareTag(rightDot.tag) && rightDot.CompareTag(gameObject.tag))
            {
                if (gameObject != leftDot && gameObject != rightDot && leftDot != rightDot)
                {
                    StartCoroutine(this.CountFindMatched(5, 0.3f, 0));
                    Debug.Log("Destroy up right left");
                }
            }
        }

        //down left right
        if (column + 1 < this.allDots.With && column - 1 >= 0 && row - 1 >= 0)
        {
            if (leftDot && rightDot && downLeftRight && downLeftRight.CompareTag(leftDot.tag)
            && leftDot.CompareTag(rightDot.tag) && rightDot.CompareTag(gameObject.tag))
            {
                if (gameObject != leftDot && gameObject != rightDot && leftDot != rightDot)
                {
                    StartCoroutine(this.CountFindMatched(5, 0.3f, 0));
                    Debug.Log("Destroy downLeftRight");
                }
            }
        }

        //down right left
        if (column + 1 < this.allDots.With && column - 1 >= 0 && row - 1 >= 0)
        {
            if (leftDot && rightDot && downRightLeft && downRightLeft.CompareTag(leftDot.tag)
            && leftDot.CompareTag(rightDot.tag) && rightDot.CompareTag(gameObject.tag))
            {
                if (gameObject != leftDot && gameObject != rightDot && leftDot != rightDot)
                {
                    StartCoroutine(this.CountFindMatched(5, 0.3f, 0));
                    Debug.Log("Destroy downRightLeft");
                }
            }
        }
    }
    #endregion

    public IEnumerator CheckDestroyMatched()
    {
        yield return null;
        if (!isMatched)
        {
            Debug.Log("Chua phu hop");
            StartCoroutine(EndGameController.Instance.GameOver());
        }
        else
        {
            StartCoroutine(grid.DestroyMatched());
            Debug.Log("Destroy");
        }
    }
}
