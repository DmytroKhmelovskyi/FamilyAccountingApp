import React from 'react';
import axios from 'axios';
import { API_BASE_URL } from '../config';
import { Container, Col, Form, Row, FormGroup, Label, Input, Button }
    from 'reactstrap';
export class PersonAdd extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            FirstName: '',
            LastName: '',
            Email: '',
            Phone: ''
        }
    }
    Addperson = () => {
        axios.post(`${API_BASE_URL}/persons/add/`, {
            FirstName: this.state.FirstName, LastName: this.state.LastName,
            Email: this.state.Email, Phone: this.state.Phone
        })
            .then(json => {
                if (json.data.Status === 'Success') {
                    console.log(json.data.Status);
                    alert("Data Save Successfully");
                    this.props.history.push('/persons')
                }
                else {
                    alert('Data not Saved');
                    debugger;
                    this.props.history.push('/persons')
                }
            })
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    render() {
        return (

            < Container className="App" >

                < h4 className="PageHeading" > Enter Student Informations</ h4 >

                < Form className="form" >

                    < Col >

                        < FormGroup row >

                            < Label for="FirstName" sm={2}> FirstName </ Label >

                            < Col sm={10}>

                                < Input type="text" name="FirstName" onChange={this.handleChange}
                                    value={this.state.FirstName}
                                    placeholder="Enter FirstName" />

                            </ Col >

                        </ FormGroup >

                        < FormGroup row >

                            < Label for="LastName" sm={2}> LastName </ Label >

                            < Col sm={10}>

                                < Input type="text" name="LastName" onChange={this.handleChange}
                                    value={this.state.LastName}
                                    placeholder="Enter LastName" />

                            </ Col >

                        </ FormGroup >

                        < FormGroup row >

                            < Label for="Email" sm={2}> Email </ Label >

                            < Col sm={10}>

                                < Input type="text" name="Email" onChange={this.handleChange}
                                    value={this.state.Email}
                                    placeholder="Enter Email" />

                            </ Col >

                        </ FormGroup >

                        < FormGroup row >

                            < Label for="Phone" sm={2}> Phone </ Label >

                            < Col sm={10}>

                                < Input type="text" name="Phone" onChange={this.handleChange}
                                    value={this.state.Phone}
                                    placeholder="Enter Phone" />

                            </ Col >

                        </ FormGroup >

                    </ Col >

                    < Col >

                        < FormGroup row >

                            < Col sm={5}>

                            </ Col >

                            < Col sm={1}>

                                < button type="button" onClick={this.Addperson}
                                    className="btn btn-success" > Submit </ button >

                            </ Col >

                            < Col sm={1}>

                                < Button color="danger" > Cancel </ Button >{' '}
                            </ Col >
                            < Col sm={5}>

                            </ Col >

                        </ FormGroup >

                    </ Col >

                </ Form >

            </ Container >
        );
    }

}