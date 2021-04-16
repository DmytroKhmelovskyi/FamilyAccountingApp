import React, { Component } from 'react';
import { API_BASE_URL } from '../config';
import { ButtonsList } from './Buttons';
import { Transactions } from './ListOfTransactions';

export class WalletDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            empData: {}, loading: true,
        };
    }

    componentDidMount() {
        this.populateWalletsData();
    }
    static renderWalletsData(empData) {
        var rghtmrgnStyle = {
            margin: "0px 10px 0px 0px"
        };
        var botmrgnStyle = {
            margin: "0px 0px 10px 0px"
        };
        return (
            <div>
                <h1>Wallet Details</h1>
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
                    <td>{empData.description}</td>
                    <td>{empData.IsActive}</td>
                    <td>{empData.balance}</td>
                    <td>{empData.expense}</td>
                    <td>{empData.income}</td>
                    <td>{empData.iscash}</td>
                </tbody>
                </table>
                <ButtonsList />
                <h1>List of transactions</h1>
                <div id='Filter?'>
                    <form method="post">
                        <div class="d-flex justify-content-end">
                            <h4 style={rghtmrgnStyle}>From date</h4>
                            <input type="date" id="from" name="from" class="form-control" style={rghtmrgnStyle} />
                            <h4 style={rghtmrgnStyle}>To date</h4>
                         <input type="date" id="to" name="to" class="form-control" />
                            <input type="number" id="id" name="id" class="form-control" value="1" />
                            <input type="submit" value="Apply filter" class="btn btn-success" style={botmrgnStyle} />
                        </div>
                    </form>                
                </div>
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
                        {empData.transactions.map(trans =>
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
                <div id='Pagination?'>
                    <div class="pagination-container">
                        <ul class="pagination">
                            <li class="page-item active">
                                <span class="page-link">1</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
    render() {
        console.log(API_BASE_URL)

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : WalletDetails.renderWalletsData(this.state.empData);

        return (
            <div>
                {contents}
            </div>
        );
    }


    async populateWalletsData() {
        const response = await fetch(`${API_BASE_URL}/wallets/details/` + this.props.match.params.walletid);
        const data = await response.json();
        this.setState({ empData: data, loading: false });
    }
}