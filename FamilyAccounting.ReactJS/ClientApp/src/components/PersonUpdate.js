import React from 'react';
import { API_BASE_URL } from '../config';
import { Container, Col, Form, Row, FormGroup, Label, Input, Button } from 'reactstrap';
import axios from 'axios'

export class PersonUpdate extends React.Component {
    constructor(props) {
        super(props)

        this.onChangeFirstName = this.onChangeFirstName.bind(this);
        this.onChangeLastName = this.onChangeLastName.bind(this);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangePhone = this.onChangePhone.bind(this);
        this.onSubmit = this.onSubmit.bind(this);

        this.state = {
            FirstName: '',
            LastName: '',
            Email: '',
            Phone: ''

        }
    }

    componentDidMount() {
        axios.get(`${API_BASE_URL}/persons/details/` + this.props.match.params.personid)
            .then(response => {
                this.setState({
                    FirstName: response.data.firstName,
                    LastName: response.data.lastName,
                    Email: response.data.email,
                    Phone: response.data.phone
                });

            })
            .catch(function (error) {
                console.log(error);
            })
    }

    onChangeFirstName(e) {
        this.setState({
            FirstName: e.target.value
        });
    }
    onChangeLastName(e) {
        this.setState({
            LastName: e.target.value
        });
    }
    onChangeEmail(e) {
        this.setState({
            Email: e.target.value
        });
    }
    onChangePhone(e) {
        this.setState({
            Phone: e.target.value
        });
    }

    onSubmit(e) {
        debugger;
        e.preventDefault();
        const obj = {
            Id: this.props.match.params.personid,
            FirstName: this.state.FirstName,
            LastName: this.state.LastName,
            Email: this.state.Email,
            Address: this.state.Address

        };
        axios.post(`${API_BASE_URL}/persons/update/`, obj)
            .then(res => console.log(res.data));
        debugger;
        this.props.history.push('/persons')
    }
    render() {
        return (
            <Container className="App">

                <h4 className="PageHeading">Update</h4>
                <Form className="form" onSubmit={this.onSubmit}>
                    <Col>
                        <FormGroup row>
                            <Label for="FirstName" sm={2}>FirstName</Label>
                            <Col sm={10}>
                                <Input type="text" name="FirstName" value={this.state.FirstName} onChange={this.onChangeFirstName}
                                    placeholder="Enter FirstName" />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="LastName" sm={2}>LastName</Label>
                            <Col sm={10}>
                                <Input type="text" name="LastName" value={this.state.LastName} onChange={this.onChangeLastName} placeholder="Enter LastName" />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="Email" sm={2}>Email</Label>
                            <Col sm={10}>
                                <Input type="text" name="Email" value={this.state.Email} onChange={this.onChangeEmail} placeholder="Enter Email" />
                            </Col>
                        </FormGroup>
                        <FormGroup row>
                            <Label for="Phone" sm={2}>Phone</Label>
                            <Col sm={10}>
                                <Input type="text" name="Phone" value={this.state.Phone} onChange={this.onChangePhone} placeholder="Enter Phone" />
                            </Col>
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup row>
                            <Col sm={5}>
                            </Col>
                            <Col sm={1}>
                                <Button type="submit" color="success">Submit</Button>{' '}
                            </Col>
                            <Col sm={1}>
                                <Button color="danger">Cancel</Button>{' '}
                            </Col>
                            <Col sm={5}>
                            </Col>
                        </FormGroup>
                    </Col>
                </Form>
            </Container>
        );
    }

}


//import * as React from 'react';
//import { RouteComponentProps } from 'react-router';
//import { Link, NavLink } from 'react-router-dom';
//import { API_BASE_URL } from '../config';



//export class PersonUpdate extends React.Component {
//    constructor(props) {
//        super(props);

//        this.state = {
//            title: "", loading: true, empData: {}
//        };

//        var personid = this.props.match.params["id"];

//        // This will set state for Edit employee  
//        if (personid > 0) {
//            fetch(`${API_BASE_URL}/persons/details/` + this.props.match.params.personid)
//                .then(response => response.json())
//                .then(data => {
//                    this.setState({ title: "Update", loading: false, empData: data });
//                });
//        }
//        // This binding is necessary to make "this" work in the callback  
//        this.handleSave = this.handleSave.bind(this);
//        this.handleCancel = this.handleCancel.bind(this);
//    }
//    componentDidMount() { populatePersonsData; }

//    render() {
//        let contents = this.state.loading
//            ? <p><em>Loading...</em></p>
//            : this.renderCreateForm(this.state.person);

//        return <div>
//            <h1>{this.state.title}</h1>
//            <h3>Person</h3>
//            <hr />
//            {contents}
//        </div>;
//    }

//    // This will handle the submit form event.  
//    handleSave(event) {
//        event.preventDefault();
//        const data = new FormData(event.target);
//    }
//    // PUT request for Edit employee.  
//    async populatePersonsData() {
//        this.state.empData.person.id
//        fetch(`${API_BASE_URL}/persons/Update/` + this.props.match.params.personid, {
//            method: 'PUT',
//            body: data,

//        }).then((response) => response.json())
//            .then((responseJson) => {
//                this.props.history.push("/persons");
//            });
//    }

//    // This will handle Cancel button click event.  
//    handleCancel(e) {
//        e.preventDefault();
//        this.props.history.push("/persons");
//    }

//    // Returns the HTML Form to the render() method.  
//    renderCreateForm() {
//        return (
//            <form onSubmit={this.handleSave} >
//                < div className="form-group row" >
//                    <label className=" control-label col-md-12" htmlFor="FirstName">FirstName</label>
//                    <div className="col-md-4">
//                        <input className="form-control" type="text" name="FirstName" defaultValue={this.state.empData.person.firstName} required />
//                    </div>
//                </div >
//                <div className="form-group row">
//                    <label className="control-label col-md-12" htmlFor="LastName" >LastName</label>
//                    <div className="col-md-4">
//                        <input className="form-control" type="text" name="LastName" defaultValue={this.state.empData.person.lastName} required />
//                    </div>
//                </div>
//                <div className="form-group row">
//                    <label className="control-label col-md-12" htmlFor="Email" >Email</label>
//                    <div className="col-md-4">
//                        <input className="form-control" type="text" name="Email" defaultValue={this.state.empData.person.email} required />
//                    </div>
//                </div>
//                <div className="form-group row">
//                    <label className="control-label col-md-12" htmlFor="Phone" >Phone</label>
//                    <div className="col-md-4">
//                        <input className="form-control" type="text" name="Phone" defaultValue={this.state.empData.person.phone} required />
//                    </div>
//                </div>
//                <div className="form-group">
//                    <button type="submit" className="btn btn-default">Save</button>
//                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
//                </div >
//            </form >
//        )
//    }


//}