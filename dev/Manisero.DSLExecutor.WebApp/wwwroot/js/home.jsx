var Body = React.createClass({
    getInitialState: function () {
        return {
            outputs: []
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
                    outputs: response.Result
                });
            });
    },
    render: function () {
        var outputs = this.state.outputs.map(function (output, i) {
            return <div key={i}>{output}</div>
        });

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
                    {outputs}
                </div>
            </div>
	    );
    }
});

ReactDOM.render(
    <Body />,
    document.getElementById('body')
);
