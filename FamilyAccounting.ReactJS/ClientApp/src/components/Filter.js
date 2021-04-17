import React from "react";


export class Filter extends React.Component {
    render() {
        var rghtmrgnStyle = {
            margin: "0px 10px 0px 0px"
        };
        var botmrgnStyle = {
            margin: "0px 0px 10px 0px"
        };
        var noneStyle = {
            display: "none"
        };
        return (
            <form method="post">
                <div class="d-flex justify-content-end">
                    <h4 style={rghtmrgnStyle}>From date</h4>
                    <input type="date" id="from" name="from" class="form-control" style={rghtmrgnStyle} />
                    <h4 style={rghtmrgnStyle}>To date</h4>
                    <input type="date" id="to" name="to" class="form-control" style={rghtmrgnStyle} />
                    <input type="number" id="id" name="id" class="form-control" style={noneStyle} />
                    <input type="submit" value="Apply filter" class="btn btn-success" style={botmrgnStyle} />
                </div>
            </form>
        );
    }
}