import React from "react";


export class Pagination extends React.Component {
    render() {
        return (
            <div class="pagination-container">
                <ul class="pagination">
                    <li class="page-item active">
                        <span class="page-link">1</span>
                    </li>
                </ul>
            </div>
        );
    }
}