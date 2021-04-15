import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Person } from './components/Person';
import { PersonDetails } from './components/PersonDetails';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
            <Route exact path='/persons' component={Person} />
            <Route exact path='/persons/details/:personid' component={PersonDetails} />  
      </Layout>
    );
  }
}
