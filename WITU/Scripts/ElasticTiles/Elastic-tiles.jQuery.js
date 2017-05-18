(function($) {

$.fn.tiles = function() {

	var element = this;

	$(window).on('load', function() {
		createTiles();
	})
	
	$(window).bind('resize', function() {
		createTiles();
	})
	
	function createTiles() {

		var windowHeight = $(window).height();
		var windowWidth = $(window).width();	
		var rows = element.children('ul');
		var numberOfRows = rows.length;
		var rowHeight = windowHeight/numberOfRows;		
		
		$(rows).each(function() {
					
					var $this = $(this);
					var tile = $this.children('li');
					var numberOfTiles = tile.length;
					var tileWidths = windowWidth/numberOfTiles;
					
					tile.css({"width" : tileWidths});
					
					// remove row if display is set to none
					
					if (!($this).is(':visible')) {
						
						numberOfRows = numberOfRows - 1;
						rowHeight = windowHeight/numberOfRows;
					}
				});
				
		$(rows).css({"height": rowHeight});
		
	}

}

})(jQuery);

