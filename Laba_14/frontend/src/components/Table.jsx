import  { useState, useEffect } from "react";

const Table = () => {
    const [products, setProducts] = useState([])

    useEffect(() => {
        fetch("https://localhost:7039/api/products")
            .then(response => {
                if (!response.ok)
                    throw new Error('Ошибка загрузки данных')
                return response.json()
            })
            .then(data => {
                setProducts(data)
            })
            .catch(err => {
                throw new Error(err)
            })
    }, []);

    return (
        <div style={{margin: '20px', fontFamily: 'Arial, sans-serif'}}>
            <h2 style={{color: '#1a237e'}}>Список товаров</h2>
            <table style={{width: '100%', borderCollapse: 'collapse', boxShadow: '0 0 20px rgba(33,150,243,0.1)'}}>
                <thead>
                <tr style={{backgroundColor: '#e3f2fd'}}>
                    <th style={tableHeaderStyle}>Id</th>
                    <th style={tableHeaderStyle}>Имя</th>
                    <th style={tableHeaderStyle}>Описание</th>
                    <th style={tableHeaderStyle}>Цена</th>
                    <th style={tableHeaderStyle}>Количество на складе</th>
                </tr>
                </thead>

                <tbody>
                {products.map((product, index) => (
                    <tr key={product.id} style={index % 2 === 0 ? evenRowStyle : oddRowStyle}>
                        <td style={tableCellStyle}>{product.id}</td>
                        <td style={tableCellStyle}>{product.name}</td>
                        <td style={tableCellStyle}>{product.description}</td>
                        <td style={tableCellStyle}>{product.price} $</td>
                        <td style={tableCellStyle}>{product.stock} шт.</td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    )
}

const tableHeaderStyle = {
    padding: '12px',
    textAlign: 'left',
    borderBottom: '2px solid #90caf9',
    fontSize: '16px',
    color: '#0d47a1',
    fontWeight: '600'
}

const tableCellStyle = {
    padding: '12px',
    textAlign: 'left',
    borderBottom: '1px solid #bbdefb',
    fontSize: '14px',
    color: '#1a237e'
}

const evenRowStyle = {
    backgroundColor: '#f0f4ff'
}

const oddRowStyle = {
    backgroundColor: '#fff',
    transition: 'background-color 0.3s ease',
    ':hover': {
        backgroundColor: '#e3f2fd'
    }
}

export default Table