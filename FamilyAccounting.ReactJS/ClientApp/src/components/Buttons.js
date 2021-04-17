import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Popup } from './PopUpWindow';
import { Transactions } from './ListOfTransactions';
import { Filter } from './Filter';
import './popup.css';
import { Pagination } from './Pagination';

export class ButtonsList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showMakeIncome: false,
            showMakeExpense: false,
            showMakeTransfer: false,
            showSetInitialBalance: false,
            showUpdateWallet: false,
            showDeleteWallet: false,
            showSeeCard: false
        }
    }
    toggleMakeIncome() {
        this.setState({
            showMakeIncome: !this.state.showMakeIncome
        });
    }
    toggleMakeExpense() {
        this.setState({
            showMakeExpense: !this.state.showMakeExpense
        });
    }
    toggleMakeTransfer() {
        this.setState({
            showMakeTransfer: !this.state.showMakeTransfer
        });
    }
    toggleSetInitialBalance() {
        this.setState({
            showSetInitialBalance: !this.state.showSetInitialBalance
        });
    }
    toggleUpdateWallet() {
        this.setState({
            showUpdateWallet: !this.state.showUpdateWallet
        });
    }
    toggleDeleteWallet() {
        this.setState({
            showDeleteWallet: !this.state.showDeleteWallet
        });
    }
    toggleSeeCard() {
        this.setState({
            showSeeCard: !this.state.showSeeCard
        });
    }
    render() {
        var lowerbtnStyle = {
            margin: "10px 10px 0px 0px"
        };
        var upperbtnStyle = {
            margin: "0px 10px 0px 0px"
        };
        return (
            <div className="app">
                <div>
                    <button onClick={this.toggleMakeIncome.bind(this)} type="button" class="btn btn-outline-success" style={upperbtnStyle}>Make income
                </button>
                    <button onClick={this.toggleMakeExpense.bind(this)} type="button" class="btn btn-outline-info" style={upperbtnStyle}>Make expense
                </button>
                    <button onClick={this.toggleMakeTransfer.bind(this)} type="button" class="btn btn-outline-success" style={upperbtnStyle}>Make transfer
                </button>
                    <button onClick={this.toggleSetInitialBalance.bind(this)} type="button" class="btn btn-outline-warning" style={upperbtnStyle}>Set initial balance
                </button>
                    <button onClick={this.toggleUpdateWallet.bind(this)} type="button" class="btn btn-outline-warning" style={upperbtnStyle}>Update wallet
                </button>
                    <button onClick={this.toggleDeleteWallet.bind(this)} type="button" class="btn btn-outline-danger">Delete wallet
                </button>
                </div>
                <div class="btn-group btn-group-lg">
                    <button onClick={this.toggleSeeCard.bind(this)} type="button" class="btn btn-outline-primary" style={lowerbtnStyle}>See card
                </button>
                    <Link to={`/persons/details/${this.props.dataFromParent.personId}`} type="button" class="btn btn-outline-primary" style={lowerbtnStyle}>Back to person
                </Link>
                </div>              
                <h1>List of transactions</h1>
                <Filter />
                <Transactions dataFromParent={this.props.dataFromParent} />
                <Pagination />
                <div>
                {this.state.showMakeIncome ?
                    <Popup
                        text='Close Me1'
                        closePopup={this.toggleMakeIncome.bind(this)}
                    />
                    : null
                }
                {this.state.showMakeExpense ?
                    <Popup
                        text='Close Me2'
                        closePopup={this.toggleMakeExpense.bind(this)}
                    />
                    : null
                }
                {this.state.showMakeTransfer ?
                    <Popup
                        text='Close Me3'
                        closePopup={this.toggleMakeTransfer.bind(this)}
                    />
                    : null
                }
                {this.state.showSetInitialBalance ?
                    <Popup
                        text='Close Me4'
                        closePopup={this.toggleSetInitialBalance.bind(this)}
                    />
                    : null
                }
                {this.state.showUpdateWallet ?
                    <Popup
                        text='Close Me5'
                        closePopup={this.toggleUpdateWallet.bind(this)}
                    />
                    : null
                }
                {this.state.showDeleteWallet ?
                    <Popup
                        text='Close Me6'
                        closePopup={this.toggleDeleteWallet.bind(this)}
                    />
                    : null
                }
                {this.state.showSeeCard ?
                    <Popup
                        text='Close Me7'
                        closePopup={this.toggleSeeCard.bind(this)}
                    />
                    : null
                    }
                    </div>
            </div>
        );
    }   
}