import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Person } from './components/Person';
import { PersonDetails } from './components/PersonDetails';
import { WalletDetails } from './components/WalletDetails';
import { Transactions } from './components/ListOfTransactions';


import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
            <Route exact path='/persons' component={Person} />
            <Route exact path='/persons/details/:personid' component={PersonDetails} />
            <Route exact path='/wallets/details/:walletid' component={WalletDetails} />
            <Route exact path='/transactionz/:walletid' component={Transactions} />
      </Layout>
    );
  }
}
