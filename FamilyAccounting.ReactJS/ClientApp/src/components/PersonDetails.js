import React, { Component } from 'react';
import { API_BASE_URL } from '../config';

export class PersonDetails extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            empData: {}, loading: true ,};
        //this.handleDetails = this.handleDetails.bind(this);  

    }
    //render() {
    //    // получаем параметры
    //    const personid = this.props.match.params.personid;
    //    return <h2>id: {personid}  Name:persons. </h2>;
    //}


    componentDidMount() {
        this.populatePersonsData();
    }
    static renderPersonsData(empData) {
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
                    <td>{empData.firstName} {empData.lastName}</td>
                    <td>{empData.email}</td>
                    <td>{empData.phone}</td>
                    <td>{empData.balance}</td>
                </tbody>
            </table>
          
        );
    }
    render() {
        console.log(API_BASE_URL)

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : PersonDetails.renderPersonsData(this.state.empData);

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
        this.setState({ empData: data, loading: false });
    }
}
//export class PersonDetails extends Component {
//    static displayName = PersonDetails.name;

//    constructor(props) {
//        super(props);
//        this.state = { persons: [], loading: true };
//        //this.handleDetails = this.handleDetails.bind(this);  

//    }

//    componentDidMount() {
//        this.populatePersonsData();
//    }


//    // handleDetails(id) {
//    //    this.props.history.push("/persons/details/" + id);
//    //}  


   

//    render() {
//        console.log(API_BASE_URL)
//        let contents = this.state.loading
//            ? <p><em>Loading...</em></p>
//            : PersonDetails.renderPersonsData(this.state.persons);

//        return (
//            <div>
//                <h1 id="tabelLabel" >Persons</h1>
//                <p>This component demonstrates fetching data from the server.</p>
//                {contents}
//            </div>
//        );
//    }

//    async populatePersonsData() {
//        const response = await fetch(`${API_BASE_URL}/persons/details/:personid`);
//        const data = await response.json();
//        this.setState({ persons: data, loading: false });
//    }
//}
