import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link, NavLink } from 'react-router-dom';
import { API_BASE_URL } from '../config';



export class PersonUpdate extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            title: "", loading: true, empData: {}
        };

        var personid = this.props.match.params["id"];

        // This will set state for Edit employee  
        if (personid > 0) {
            fetch(`${API_BASE_URL}/persons/details/` + this.props.match.params.personid)
                .then(response => response.json())
                .then(data => {
                    this.setState({ title: "Update", loading: false, empData: data });
                });
        }
        // This binding is necessary to make "this" work in the callback  
        this.handleSave = this.handleSave.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }
    componentDidMount() { populatePersonsData; }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm(this.state.person);

        return <div>
            <h1>{this.state.title}</h1>
            <h3>Person</h3>
            <hr />
            {contents}
        </div>;
    }

    // This will handle the submit form event.  
    handleSave(event) {
        event.preventDefault();
        const data = new FormData(event.target);
    }
    // PUT request for Edit employee.  
    async populatePersonsData() {
        this.state.empData.person.id
        fetch(`${API_BASE_URL}/persons/Update/` + this.props.match.params.personid, {
            method: 'PUT',
            body: data,

        }).then((response) => response.json())
            .then((responseJson) => {
                this.props.history.push("/persons");
            });
    }

    // This will handle Cancel button click event.  
    handleCancel(e) {
        e.preventDefault();
        this.props.history.push("/persons");
    }

    // Returns the HTML Form to the render() method.  
    renderCreateForm() {
        return (
            <form onSubmit={this.handleSave} >
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="FirstName">FirstName</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="FirstName" defaultValue={this.state.empData.person.firstName} required />
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="LastName" >LastName</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="LastName" defaultValue={this.state.empData.person.lastName} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Email" >Email</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="Email" defaultValue={this.state.empData.person.email} required />
                    </div>
                </div>
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Phone" >Phone</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="Phone" defaultValue={this.state.empData.person.phone} required />
                    </div>
                </div>
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    }


}