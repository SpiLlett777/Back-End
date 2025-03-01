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
            <h2>Список товаров</h2>
            <table style={{width: '100%', borderCollapse: 'collapse'}}>
                <thead>
                <tr style={{backgroundColor: '#f5f5f5'}}>
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
    borderBottom: '2px solid #ddd',
    fontSize: '16px'
}

const tableCellStyle = {
    padding: '12px',
    textAlign: 'left',
    borderBottom: '1px solid #ddd',
    fontSize: '14px'
}

const evenRowStyle = {
    backgroundColor: '#f9f9f9'
}

const oddRowStyle = {
    backgroundColor: '#fff'
}

export default Table