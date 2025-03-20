import { useState } from 'react'
import Table from "./components/Table.jsx";

function App() {
  const [count, setCount] = useState(0)

  return (
    <Table />
  )
}

export default App
