var Body = React.createClass({
    render: function() {
	    return (
            <div className="row">
                <div className="col-md-5">
                    <h2>Input</h2>
                    <input type="text" />
                </div>
                <div className="col-md-2">
                    <input type="button" value="Run" />
                </div>
                <div className="col-md-5">
                    <h2>Output</h2>
                    <div>output...</div>
                </div>
            </div>
	    );
    }
});

ReactDOM.render(
    <Body />,
    document.getElementById('body')
);
