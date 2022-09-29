using DelegateSample;

var processor = new PhotoProcessor();

PhotoFilters filters = new PhotoFilters();
PhotoProcessor.PhotoFilterHandler filterHandler = filters.ApplyBrightness;

processor.Process("photo.jpg", filterHandler);