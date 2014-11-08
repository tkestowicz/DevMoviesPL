/**
 * @jsx React.DOM
 */

 var VideosOrder = React.createClass({

 	getInitialState: function(){

 		var currentField = this.props.orders.filter(function(field){
 			return field.field === this.props.currentSettings.PropertyName;
 		}, this);

 		return {
 			current: {
 				field: currentField[0],
 				direction: this.props.currentSettings.Direction
 			}
 		};
 	},

 	sortOrderChanged: function(event){

 		var target = event.target;

 		event.preventDefault();

 		if(event.target.nodeName === "SPAN")
 		{
 			target = event.target.parentElement;
 		}

 		var data = $(target).data(),
 			currentField = this.props.orders.filter(function(field){
 				return field.field === data.field;
 			}, this);

 		this.setState({ current: {
 			field: currentField[0],
 			direction: data.orderDirection
 		}});

 		$('#'+this.props.componentName)
 		.trigger("changeOrder", {
 				PropertyName: data.field,
 				Direction: data.orderDirection
		});
 	},

 	render: function(){
 		
 		var icon = (this.state.current.direction === this.props.directions.ascending) 
 						? 'glyphicon glyphicon-sort-by-attributes'
 						: 'glyphicon glyphicon-sort-by-attributes-alt',
 			options = this.props.orders.map(function(field){

 				var active = field === this.state.current.field;

	 			return (<div>
			                <li className="divider"></li>
				            <li className={active && this.state.current.direction == this.props.directions.ascending ? 'active' : '' }>
				            	<a href="#" onClick={this.sortOrderChanged} data-order-direction={this.props.directions.ascending} data-field={field.field}>{field.label} <span className="glyphicon glyphicon-sort-by-attributes"></span></a>
			            	</li>
				            <li className={active && this.state.current.direction == this.props.directions.descending ? 'active' : '' }>
				            	<a href="#" onClick={this.sortOrderChanged}  data-order-direction={this.props.directions.descending} data-field={field.field}>{field.label} <span className="glyphicon glyphicon-sort-by-attributes-alt"></span></a>
			            	</li>
		            	</div>);
 			}.bind(this));

 		return (
 			    <div id={this.props.componentName} className="pull-right  col-sm-4 col-md-4 col-lg-4 text-right order">
			        Kolejność: 
			        <div className="btn-group">
			            <button type="button" className="btn btn-primary btn-xs dropdown-toggle" data-toggle="dropdown">
			                {this.state.current.field.label} <span className={icon}></span> <span className="caret"></span>
			            </button>
			            <ul className="dropdown-menu" role="menu">
			                {options}
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