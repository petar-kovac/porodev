import axios from 'axios';
import { FC, ReactNode, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { IFilesCard } from 'types/card-data';
import RuntimeCard from 'components/card/RuntimeCard';
import { startRuntimeService } from 'service/runtime/runtime';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import { Input } from 'antd';
import dayjs from 'dayjs';

const Runtime: FC = () => {
  const [data, setData] = useState<IFilesCard[] | undefined>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [cardData, setCardData] = useState<IFilesCard | undefined>(undefined);
  const [content, setContent] = useState<ReactNode>(undefined);
  const count = useRef(0);

  const onClick = useCallback((value: any) => {
    count.current += 1;
    setCardData(value);
    setTimeout(() => {
      if (count.current === 1) {
        setIsSiderVisible(true);
      }

      if (count.current === 2) {
        setIsSiderVisible(false);
        setIsModalVisible(true);
      }

      count.current = 0;
    }, 250);
  }, []);

  const startRuntime = async () => {
    const res = await startRuntimeService({
      fileID: 'b22a0496-ded5-4fe5-bbb3-e20280b70a03',
      jwt: localStorage.getItem('accessToken'),
    });

    // hardcoded for backend demo
    setContent(
      <div>
        <p>Execution has happened : {`${res.exceptionHappened}`}</p>
        <p>Execution output : {res.executionOutput}</p>
        <p>Execution time : {res.executionTime}</p>
        <p>
          Execution start :
          {dayjs.unix(Date.parse(res.executionStart)).format('D MMMM')}
        </p>
      </div>,
    );
    setIsSiderVisible(false);
    setIsModalVisible(true);
  };

  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/files`);
        setData(res.data);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return (
    <StyledPageWrapper>
      <StyledContent>
        <StyledStaticContent>
          <StyledFilesWrapper>
            {data?.slice(0, 3).map((value: any) => (
              <>
                <RuntimeCard
                  title={value?.title}
                  createdAt={value?.createdAt}
                  onClick={() => onClick(value)}
                />
              </>
            ))}
          </StyledFilesWrapper>
        </StyledStaticContent>

        <PFileSider
          cardData={cardData}
          isSiderVisible={isSiderVisible}
          setIsSiderVisible={setIsSiderVisible}
          type="runtime"
          onButtonClick={startRuntime}
        />
      </StyledContent>

      <PModal
        title="Result of you action: "
        isModalVisible={isModalVisible}
        cardData={cardData}
        setIsModalVisible={setIsModalVisible}
        content={content}
      />
    </StyledPageWrapper>
  );
};

const StyledPageWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const StyledContent = styled.div`
  display: flex;
`;

const StyledStaticContent = styled.div`
  display: flex;
  flex-direction: column;
  width: 80%;
  height: 100vh;
  margin: 0 auto;
  align-items: flex-start;
  gap: 5rem;
  padding: 4rem 0;
`;

const StyledFilesWrapper = styled.div`
  padding: 0 2rem;
  display: flex;
  justify-content: center;
  gap: 2.5rem;
  flex-wrap: wrap;
`;

export default Runtime;
