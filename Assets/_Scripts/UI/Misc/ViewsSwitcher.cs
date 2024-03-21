using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewsSwitcher
{
    //==========================
    //=====FIELDS
    //==========================

    private Canvas[] viewsArray;
    private Canvas viewCurrent;
    private Canvas viewPrevious;

    //==========================
    //=====CONSTRUCTOR
    //==========================

    public ViewsSwitcher(params Canvas[] views)
    {
        viewsArray = views;
    }

    //==========================
    //=====API
    //==========================

    //Switch views
    public void Switch(Canvas view)
    {
        if(viewsArray.Contains(view))
        {
            HideAll();
            Show(view);

            viewPrevious = viewCurrent;
            viewCurrent = view;
        }
    }
    public void Switch(int index)
    {        
        var view = viewsArray[index];
        if (viewsArray.Contains(view))
        {
            HideAll();
            Show(view);

            viewPrevious = viewCurrent;
            viewCurrent = view;
        }
    }

    public void Back()
    {
        Switch(viewPrevious);
    }

    //==========================
    //=====METHODS
    //==========================

    private void Show(Canvas view)
    {
        view.enabled = true;
    }
    private void Hide(Canvas view)
    {
        view.enabled = false;
    }

    private void HideAll()
    {
        foreach (var view in viewsArray)
        {
            Hide(view);
        }
    }
}
