import React, { Component } from 'react';
import { API_BASE_URL } from '../config';
import { Link } from 'react-router-dom';

export class PersonDetails extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            personDetails: {}, loading: true ,};
    }

    componentDidMount() {
        this.populatePersonsData();
    }
 

    static renderPersonsData(personDetails) {
        return (

            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>Balance</th>
                    </tr>
                </thead>
                <tbody>
                    <td>{personDetails.firstName} {personDetails.lastName}</td>
                    <td>{personDetails.email}</td>
                    <td>{personDetails.phone}</td>
                    <td>{personDetails.balance}</td>
                    <table>
                    <thead>
                        <tr>
                            <th>Description</th>
                                <th>Balance</th>
                        </tr>
                    </thead>
                    <tbody>
                    <td>
                        {personDetails.wallets.map(person =>
                            <tr key={person.id}>
                                
                                <td>{person.description}</td>
                                <td>{person.balance}</td>
                               
                            </tr>
                        )}
                            
                        </td> 
                        </tbody>
                        </table>

                </tbody>
            </table>
        );
    }
    render() {
        console.log(API_BASE_URL)

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : PersonDetails.renderPersonsData(this.state.personDetails);

        return (
            <div>
                <h1 id="tabelLabel" >Details</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

  
    async populatePersonsData() {
        const response = await fetch(`${API_BASE_URL}/persons/details/` + this.props.match.params.personid);
        const data = await response.json();
        this.setState({ personDetails: data, loading: false });
    }
}
