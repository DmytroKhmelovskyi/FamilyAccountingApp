import React, { Component } from 'react';
import { API_BASE_URL } from '../config';

export class Wallets extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <table className='table table-striped table-primary' aria-labelledby="tabelLabel">
                    <thead className='thead-dark'>
                        <tr>
                            <th>Description</th>
                            <th>IsActive</th>
                            <th>Balance</th>
                            <th>Expense</th>
                            <th>Income</th>
                            <th>IsCash</th>
                        </tr>
                    </thead>
                    <tbody>
                        <td>{this.props.dataFromParent.description}</td>
                        <td>{this.props.dataFromParent.IsActive}</td>
                        <td>{this.props.dataFromParent.balance}</td>
                        <td>{this.props.dataFromParent.expense}</td>
                        <td>{this.props.dataFromParent.income}</td>
                        <td>{this.props.dataFromParent.iscash}</td>
                    </tbody>
                </table>
            </div>
        );
    }
}