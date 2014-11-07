
var ChannelFilters = React.createClass({

	channelSelected: function(event){
		this.setState({ current: event.target });		
		this.props.onChannelSelected(event.target.value);
	},

  	componentWillReceiveProps: function(nextProps){
  		if(this.props.resetState && !nextProps.resetState)
  		{
  			this.reset();
  		}
  	},

  	reset: function(){
  		this.state.current.checked = false;
  		this.setState({});
  	},

	render: function(){

		var channels = this.props.channels.map(function(channel){

			return (<div className="radio">
							<label>
								<input type="radio" name="channel" value={channel} onChange={this.channelSelected} />
								{channel}
							</label>
						</div>);
		}.bind(this));

		return (<div className="panel panel-success">
				<div className="panel-heading">
					<h3 className="panel-title">Kanał</h3>
				</div>
				<div className="panel-body">
					{channels}
				</div>
			</div>);
	}
});

var PublicationYearsFilters = React.createClass({

	yearSelected: function(event){
		var year = parseInt(event.target.value);

		this.setState({ current: event.target });
		this.props.onPublicationYearSelected(year);
	},

  	componentWillReceiveProps: function(nextProps){
  		if(this.props.resetState && !nextProps.resetState)
  		{
  			this.reset();
  		}
  	},

  	reset: function(){

  		this.state.current.checked = false;
  		this.setState({});

  	},

	render: function(){

		var publicationYears = this.props.years.map(function(year){

			return (<div className="radio">
							<label>
								<input type="radio" name="publicationYear" value={year} onChange={this.yearSelected} />
								{year}
							</label>
						</div>);
		}.bind(this));

		return (<div className="panel panel-info">
				<div className="panel-heading">
					<h3 className="panel-title">Rok publikacji</h3>
				</div>
				<div className="panel-body">
					{publicationYears}
				</div>
			</div>);
	}
});

var TagsFilters = React.createClass({

	getInitialState: function() {
	    return { tags: [] };
  	},

  	componentWillReceiveProps: function(nextProps){
  		if(this.props.resetState && !nextProps.resetState)
  		{
  			this.setState(this.getInitialState());
  		}
  	},

  	approved: function(event){
  		var tags = this.state.tags,
  			value = event.target.value;

  		if(event.key === 'Enter' 
  			&& value 
  			&& this.props.tags.indexOf(value) !== -1){
  			
  				tags.push(value);
  				this.setState({ tags: tags });

  				this.props.onTagsChanged(tags);

  				event.target.value = null;
  			}
  	},

  	remove: function(event){
  		var tags = this.state.tags,
  			index = tags.indexOf(event.target.value);

  		tags.splice(index, 1);

  		this.setState({ tags: tags });

  		this.props.onTagsChanged(tags);
  	},

	render: function(){

		var self = this,
			tags = this.state.tags.map(function(tag){
			return (<span className="label label-primary pull-left" onClick={self.remove}>{tag} <span className="delete">&times;</span></span>);
		});

		return (<div className="panel panel-primary filters">
            <div className="panel-heading">
                <h3 className="panel-title">Tagi</h3>
            </div>
            <div className="panel-body">
                <div className="form-group">
                    <div className="input-group">
                        <span className="input-group-addon"><span className="glyphicon glyphicon-tag"></span></span>
                        <input type="text" className="form-control" placeholder="Wpisz tag" ref="tag" onKeyDown={this.approved} />
                    </div>
                </div>
                <div>
                    {tags}
                </div>
            </div></div>);
	}
});

var FiltersButtons = React.createClass({

	handle: function(event){
		
		var action = event.target.name;

		if(event.target.parentElement.tagName === "BUTTON")
			action = event.target.parentElement.name;

		this.props.onAction(action);
	},
	render: function() {
		return (
			<div className="btn-group btn-group-justified">
	            <div className="btn-group">
	                <button className="btn btn-primary btn-group-lg" name="filter" onClick={this.handle}><span className="glyphicon glyphicon-ok"></span> Zastosuj filtry</button>
	            </div>
	            <div className="btn-group">
	                <button className="btn btn-danger btn-group-lg" name="clear" onClick={this.handle}><span className="glyphicon glyphicon-remove"></span> Usuń filtry</button>
	            </div>
        	</div>
		);
	}
});

var VideosFilters = React.createClass({
	getInitialState: function() {
	    return {
	    	resetState: false,
	    	filters: {}
	    };
  	},
	channelSelected: function(channel){
		this.setState($.extend(this.state.filters, { ChannelName: channel }));
		console.log(this.state);
	},
	publicationYearSelected: function(year){
		this.setState($.extend(this.state.filters, { PublicationYear: year }));
		console.log(this.state);
	},
	tagsChanged: function(tags){
		this.setState($.extend(this.state.filters, { Tags: tags }));
		console.log(this.state);
	},

	actionHandler: function(action){

		var self = this,
			hasAnyFilters = function(){
				return JSON.stringify(self.state.filters) !== JSON.stringify({});
			};

		if(action === 'filter' && hasAnyFilters())
		{
			this.setState($.extend(this.state, { resetState: true }));
			$.get(this.props.url, this.state.filters, function(result){
				console.log(result);
			});
		}
		else if (action === 'clear')
		{
			this.setState(this.getInitialState());
		}
	},
	render: function(){
		
		return (<div>
					<ChannelFilters onChannelSelected={this.channelSelected} resetState={this.state.resetState} channels={this.props.data.Channels} />
					<PublicationYearsFilters onPublicationYearSelected={this.publicationYearSelected} resetState={this.state.resetState} years={this.props.data.PublicationYears} />
					<TagsFilters onTagsChanged={this.tagsChanged} resetState={this.state.resetState} tags={this.props.data.Tags} />
					<FiltersButtons onAction={this.actionHandler} />
				</div>);
	}
});