﻿using Framework.Application;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public interface ISlideApplication
    {
        public OperationResult Create(CreateSlide slide);
        public OperationResult Edit(EditSlide slide);
        public SlideViewModel Get(long id);
        public List<SlideViewModel> List();
        public List<SlideViewModel> Search(SearchSlide query);
        public EditSlide EditGet(long id);
    }
}
