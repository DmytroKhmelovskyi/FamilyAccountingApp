import React, { Component } from 'react';
import { API_BASE_URL } from '../config';

export class Transactions extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            empData: {}, loading: true,
        };
    }
    componentDidMount() {
        this.populateTransactionsData();
    }
    static renderTransactionsData(empData) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
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
                        {empData.transactions.map(person =>
                            <tr key={person.walletid}>
                                <td>{person.amount}</td>
                                <td>{person.balanceBefore}</td>
                                <td>{person.balanceAfter}</td>
                                <td>{person.catagory}</td>
                                <td>{person.timeStamp}</td>
                                <td>{person.description}</td>
                                <td>{person.transactionType}</td>
                                <td>details</td>
                            </tr>
                        )}
                        </tbody>
                </table>
            </div>
        );
    }
    render() {
        console.log(API_BASE_URL)

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Transactions.renderTransactionsData(this.state.empData.transactions);

        return (
            <div>
                {contents}
            </div>
        );
    }


    async populateTransactionsData() {
        const response = await fetch(`${API_BASE_URL}/wallets/details/` + this.props.transactions.match.params.walletid);
        const data = await response.json();
        this.setState({ empData: data, loading: false });
    }
}