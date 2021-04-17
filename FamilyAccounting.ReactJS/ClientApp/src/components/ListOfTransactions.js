import React, { Component } from 'react';
import { API_BASE_URL } from '../config';

export class Transactions extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <table className='table table-striped table-primary' aria-labelledby="tabelLabel">
                    <thead className='thead-dark'>
                        <tr>
                            <th>Amount</th>
                            <th>Before</th>
                            <th>After</th>
                            <th>Category</th>
                            <th>Date</th>
                            <th>Description</th>
                            <th>Type</th>
                            <th>Details</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.dataFromParent.transactions.map(trans =>
                            <tr key={trans.walletid}>
                                <td>{trans.amount}</td>
                                <td>{trans.balanceBefore}</td>
                                <td>{trans.balanceAfter}</td>
                                <td>{trans.catagory}</td>
                                <td>{trans.timeStamp}</td>
                                <td>{trans.description}</td>
                                <td>{trans.transactionType}</td>
                                <td>
                                    <a class="btn btn-outline-dark">👁
                                    </a>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}