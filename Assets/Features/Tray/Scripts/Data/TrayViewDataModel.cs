using System.Collections.Generic;
using HAK.UI;
using UnityEngine;

namespace HAK.Gameplay.Shape
{
    public class TrayViewDataModel: BaseViewDataModel
    {
        public ITray TrayHandler;
        public List<BaseShape> ShapeTypes;
    }
}