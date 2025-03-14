import { ChangeEvent, SyntheticEvent, useEffect, useState } from "react";
import Search from "../../Components/Search/Search.tsx";
import ListPortfolio from "../../Components/Portfolio/ListPortfolio/ListPortfolio.tsx";
import CardList from "../../Components/CardList/CardList.tsx";
import { CompanySearch } from "../../company";
import { searchCompanies } from "../../api.tsx";
import { PortfolioGet } from "../../Models/Portfolio.ts";
import {
  portfolioAddAPI,
  portfolioDeleteAPI,
  portfolioGetAPI,
} from "../../Services/PortfolioService.tsx";
import { toast } from "react-toastify";
import { ok } from "node:assert";

const SearchPage = () => {
  const [search, setSearch] = useState<string>();
  const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>(
    [],
  );
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  useEffect(() => {
    getPortfolio();
  }, []);

  const getPortfolio = () => {
    portfolioGetAPI()
      .then((respond) => {
        if (respond?.data) {
          setPortfolioValues(respond?.data);
        }
      })
      .catch((e) => {
        toast.warning("Could not get portfolio values!");
      });
  };

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();

    portfolioAddAPI(e.target[0].value)
      .then((respond) => {
        if (respond?.status == 201) {
          toast.success("Stock added to portfolio!");
          getPortfolio();
        }
      })
      .catch((e) => {
        toast.warning("Could not get portfolio values!");
      });
  };

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    portfolioDeleteAPI(e.target[0].value).then((respond) => {
      if (respond?.status === 200) {
        toast.success("Stock deleted from portfolio!");
        getPortfolio();
      }
    });
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompanies(search!);

    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };

  return (
    <>
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <ListPortfolio
        portfolioValues={portfolioValues!}
        onPortfolioDelete={onPortfolioDelete}
      />
      <CardList
        searchResults={searchResult}
        onPortfolioCreate={onPortfolioCreate}
      />
      {serverError && <div>Unable to connect to API</div>}
    </>
  );
};

export default SearchPage;
