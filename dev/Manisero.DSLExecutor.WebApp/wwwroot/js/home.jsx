var Body = React.createClass({
    getInitialState: function () {
        return {
            output: ''  
        };
    },
    handleRunClick: function () {
        var self = this;

        $.post('/Home/ProcessDSL',
            {
                DSL: this.refs.input.value
            },
            function (response) {
                self.setState({
                    output: response.Result
                });
            });
    },
    render: function () {
	    return (
            <div className="row">
                <div className="col-md-5">
                    <h2>Input</h2>
                    <textarea ref="input" />
                </div>
                <div className="col-md-2">
                    <input type="button" value="Run" onClick={this.handleRunClick} />
                </div>
                <div className="col-md-5">
                    <h2>Output</h2>
                    <div>{this.state.output}</div>
                </div>
            </div>
	    );
    }
});

ReactDOM.render(
    <Body />,
    document.getElementById('body')
);
