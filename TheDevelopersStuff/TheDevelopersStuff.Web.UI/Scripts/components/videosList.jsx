/**
 * @jsx React.DOM
 */

 var VideosOrder = React.createClass({

 	render: function(){
 		return (
 			    <div className="pull-right  col-sm-4 col-md-4 col-lg-4 text-right order">
			        Kolejność: 
			        <div className="btn-group">
			            <button type="button" className="btn btn-primary btn-xs dropdown-toggle" data-toggle="dropdown">
			                data publikacji <span className="glyphicon glyphicon-sort-by-attributes-alt"></span> <span className="caret"></span>
			            </button>
			            <ul className="dropdown-menu" role="menu">
			                <li><a href="#">data publikacji <span className="glyphicon glyphicon-sort-by-attributes"></span></a></li>
			                <li><a href="#" className="active">data publikacji <span className="glyphicon glyphicon-sort-by-attributes-alt"></span></a></li>
			                <li className="divider"></li>
			                <li><a href="#">nazwa kanału <span className="glyphicon glyphicon-sort-by-attributes"></span></a></li>
			                <li><a href="#">nazwa kanału <span className="glyphicon glyphicon-sort-by-attributes-alt"></span></a></li>
			                <li className="divider"></li>
			                <li><a href="#">tytuł <span className="glyphicon glyphicon-sort-by-attributes"></span></a></li>
			                <li><a href="#">tytuł <span className="glyphicon glyphicon-sort-by-attributes-alt"></span></a></li>
			                <li className="divider"></li>
			                <li><a href="#">najpopularniejsze <span className="glyphicon glyphicon-sort-by-attributes"></span></a></li>
			                <li><a href="#">najpopularniejsze <span className="glyphicon glyphicon-sort-by-attributes-alt"></span></a></li>
			            </ul>
			        </div>
			    </div>
 			);
 	}

 });

 var VideosList = React.createClass({

 	render: function(){
 		return (
 			<div/>
 			);
 	}

 });

var VideosPanel = React.createClass({

	render: function() {
		return (
			<div />
		);
	}

});