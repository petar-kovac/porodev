import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';
import PCard from 'components/card/PCard';
import { findFiles } from 'service/files/files';

import { DatePicker, Select, Slider, Switch } from 'antd';

const { RangePicker } = DatePicker;
const { Option } = Select;

const handleChange = (value: string) => {
  console.log(`selected ${value}`);
};

const Files: FC = () => {
  const [data, setData] = useState<[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        await axios
          .get(`${process.env.REACT_APP_MOCK_URL}/files`)
          .then((res) => setData(res.data));
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <>
      <StyledFilesWrapper>
        <StyledFilesHeader style={{}}>
          <StyledFilesDateFilter>
            <h4>Filter files by date:</h4>
            <RangePicker />
          </StyledFilesDateFilter>

          <StyledFilesSlider>
            <h4>Filter by size:</h4>
            <Slider range defaultValue={[0, 30]} />
          </StyledFilesSlider>
          <StyledFilesSelect>
            <Select defaultValue="Filter by type" onChange={handleChange}>
              <Option value="jpg">.jpg</Option>
              <Option value="png">.png</Option>
              <Option value="txt">.txt</Option>
              <Option value="pdf">.pdf</Option>
            </Select>

            <Select defaultValue="Sort by" onChange={handleChange}>
              <Option value="asc">Ascending</Option>
              <Option value="desc">Descending</Option>
              <Option value="newest" disabled>
                Newest
              </Option>
              <Option value="oldest">Oldest</Option>
            </Select>
          </StyledFilesSelect>
        </StyledFilesHeader>

        {data?.map((value: any, index: any) => (
          <PCard
            image={value?.image}
            heading={value?.name}
            description={value?.description}
          />
        ))}
      </StyledFilesWrapper>
    </>
  );
};

const StyledFilesWrapper = styled.div`
  padding: 2rem;
  display: flex;
  justify-content: center;
  gap: 2.5rem;
  flex-wrap: wrap;
`;

const StyledFilesHeader = styled.div`
  width: 100%;
  display: flex;
  align-items: flex-end;
  gap: 10px;
  justify-content: space-around;
  flex-wrap: wrap;
  padding: 2rem 0;
  background-color: #fcfcfc;
  border-radius: 3rem;
  box-shadow: 0 1px #ffffff inset, 1px 3px 8px rgba(34, 25, 25, 0.2);
  margin-bottom: 5rem;
`;

const StyledFilesSlider = styled.div`
  width: 32rem;
  .ant-slider {
    margin: 22px 6px 10px;
    padding: 0 !important;
    width: 30rem;
  }
`;

const StyledFilesDateFilter = styled.div`
  width: 32rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;

  .ant-picker {
    border-radius: 10px;
  }
`;

const StyledFilesSelect = styled.div`
  width: 32rem;
  align-self: flex-end;
  display: flex;
  gap: 1rem;

  .ant-select {
    width: 16rem;
  }

  .ant-select-selector {
    border-radius: 10px !important;
    color: #999;
  }
`;

export default Files;