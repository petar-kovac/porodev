import axios from 'axios';
import { FC, ReactNode, useCallback, useEffect, useRef, useState } from 'react';
import styled from 'styled-components';

import RuntimeCard from 'components/card/RuntimeCard';
import PModal from 'components/modal/PModal';
import PFileSider from 'layout/sider/PFileSider';
import { startRuntimeService } from 'service/runtime/runtime';
import { IFilesCard } from 'types/card-data';
import { GetRuntimeModalData } from 'util/util-components/GetRuntimeModalData';
import {
  StyledContent,
  StyledFilesWrapper,
  StyledPageWrapper,
  StyledStaticContent,
} from './runtime-styled';

const Runtime: FC = () => {
  const [data, setData] = useState<IFilesCard[] | undefined>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isSiderVisible, setIsSiderVisible] = useState(false);
  const [cardData, setCardData] = useState<IFilesCard | undefined>(undefined);
  const [content, setContent] = useState<ReactNode>(undefined);
  const [isDisabledButton, setIsDisabledButton] = useState<boolean>(false);
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
    setIsDisabledButton(true);
    const res = await startRuntimeService({
      fileID: 'b22a0496-ded5-4fe5-bbb3-e20280b70a03',
      jwt: localStorage.getItem('accessToken'),
    });
    const modalDataToRender: ReactNode = GetRuntimeModalData(res);

    setContent(modalDataToRender);
    setIsSiderVisible(false);
    setIsModalVisible(true);
    setIsDisabledButton(false);
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
                {/* <RuntimeCard
                  title={value?.title}
                  createdAt={value?.createdAt}
                  onClick={() => onClick(value)}
                /> */}
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
          isDisabledButton={isDisabledButton}
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

export default Runtime;
