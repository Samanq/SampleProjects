$(function(){
	var viewModel = {
    name: ko.observable("John"),
    changeName : function(){
			this.name("Jane");
    },
    nameVisible: ko.observable(true)
  };
  ko.applyBindings(viewModel);
});