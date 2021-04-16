import React, { Component } from 'react';

export class ButtonsList extends React.Component {
    constructor(props) {
        super(props);
    }
  
    render() {
        var lowerbtnStyle = {
            margin: "10px 10px 0px 0px"
        };
        var upperbtnStyle = {
            margin: "0px 10px 0px 0px"
        };
        return (
            <div>
                <div>
                    <button type="button" class="btn btn-outline-success" style={upperbtnStyle}>Make income
                </button>
                    <button type="button" class="btn btn-outline-info" style={upperbtnStyle}>Make expense
                </button>
                    <button type="button" class="btn btn-outline-success" style={upperbtnStyle}>Make transfer
                </button>
                    <button type="button" class="btn btn-outline-warning" style={upperbtnStyle}>Set initial balance
                </button >
                    <button type="button" class="btn btn-outline-warning" style={upperbtnStyle}>Update
                </button>
                    <button type="button" class="btn btn-outline-danger">Delet wallet
                </button>
                </div>
                <div class="btn-group btn-group-lg">
                    <button type="button" class="btn btn-outline-primary" style={lowerbtnStyle}>See card
                </button>
                    <button type="button" class="btn btn-outline-primary" style={lowerbtnStyle}>Back to person
                </button>
                    </div>
            </div>
        );
    }
}