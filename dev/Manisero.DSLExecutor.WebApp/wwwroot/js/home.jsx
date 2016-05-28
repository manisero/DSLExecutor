var Body = React.createClass({
    getInitialState: function () {
        return {
            log: [],
            result: null
        };
    },
    initialInput: "Log('Calculating...')\nSub(Add(1 2) 3)",
    handleRunClick: function () {
        var self = this;

        $.post('/Home/ProcessDSL',
            {
                DSL: this.refs.input.value
            },
            function (response) {
                self.setState({
                    log: response.Log,
                    result: response.Result
                });
            });
    },
    render: function () {
        var logs = this.state.log.map(function (log, i) {
            return <div key={i}>{log}</div>
        });

	    return (
            <div className="row">
                <div className="col-md-5">
                    <h2>Input</h2>
                    <textarea ref="input" defaultValue={this.initialInput} style={{width: '400px', height: '300px'}} />
                </div>
                <div className="col-md-2">
                    <button type="button" className="btn btn-primary" style={{ marginTop: '50px' }} onClick={this.handleRunClick}>Run</button>
                </div>
                <div className="col-md-5">
                    <h2>Result</h2>
                    {this.state.result}
                    <h2>Log</h2>
                    {logs}
                </div>
            </div>
	    );
    }
});

ReactDOM.render(
    <Body />,
    document.getElementById('body')
);
