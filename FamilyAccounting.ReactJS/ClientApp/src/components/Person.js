import React, { Component } from 'react';
import { API_BASE_URL } from '../config'

export class Person extends Component {
    static displayName = Person.name;

    constructor(props) {
        super(props);
        this.state = { persons: [], loading: true };
    }

    componentDidMount() {
        this.populatePersonsData();
    }

    static renderPersonsData(persons) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>FName</th>
                        <th>LName</th>
                        <th>WalletsCount</th>
                    </tr>
                </thead>
                <tbody>
                    {persons.map(person =>
                        <tr key={person.id}>
                            <td>{person.firstName}</td>
                            <td>{person.lastName}</td>
                            <td>{person.walletsCount}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        console.log(API_BASE_URL)
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Person.renderPersonsData(this.state.persons);

        return (
            <div>
                <h1 id="tabelLabel" >Persons</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populatePersonsData() {
        const response = await fetch(`${API_BASE_URL}/persons/getall`);
        const data = await response.json();
        this.setState({ persons: data, loading: false });
    }
}
