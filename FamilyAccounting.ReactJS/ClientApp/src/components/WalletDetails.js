import React, { Component } from 'react';
import { API_BASE_URL } from '../config';
import { ButtonsList } from './Buttons';
import { Wallets } from './ListOfWallets';

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
        return (
            <div>
                <h1>Wallet Details</h1>
                <Wallets dataFromParent={empData} />
                <ButtonsList dataFromParent={empData} />                
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