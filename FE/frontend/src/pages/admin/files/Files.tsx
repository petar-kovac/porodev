import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import styled from 'styled-components';
import { DatePicker, Select, Slider } from 'antd';

import PCard from 'components/card/PCard';

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
        <StyledFilesHeader
          style={{
            display: 'flex',
            alignItems: 'flex-end',
            gap: '10px',
            justifyContent: 'space-around',
            flexWrap: 'wrap',
          }}
        >
          <div>
            <h4>Filter files by date:</h4>
            <RangePicker />
          </div>

          <StyledFilesSlider>
            <h4>Filter by size:</h4>
            <Slider
              style={{ display: 'inline-block', width: '300px' }}
              range
              defaultValue={[20, 50]}
            />
          </StyledFilesSlider>
          <StyledFilesSelect>
            <Select
              defaultValue="Filter by type"
              style={{ width: 160 }}
              onChange={handleChange}
            >
              <Option value="jpg">.jpg</Option>
              <Option value="png">.png</Option>
              <Option value="txt">.txt</Option>
              <Option value="pdf">.pdf</Option>
            </Select>
            <Select
              defaultValue="Sort by"
              style={{ width: 160 }}
              onChange={handleChange}
            >
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
            heading={value?.name}
            description={value?.description}
            image={value?.image}
          />
        ))}
      </StyledFilesWrapper>
    </>
  );
};

const StyledFilesWrapper = styled.div`
  padding: 20px;
  gap: 10px;
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
`;

const StyledFilesHeader = styled.div`
  width: 100%;
  background-color: red;
`;

const StyledFilesSelect = styled.div``;

const StyledFilesSlider = styled.div``;

export default Files;
